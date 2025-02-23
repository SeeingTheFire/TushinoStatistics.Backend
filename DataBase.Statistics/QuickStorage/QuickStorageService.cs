using System.Text.Json;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace DataBase.Statistics.QuickStorage;

public sealed class QuickStorageService(QuickStorageConnectionProvider connectionProvider, ILoggerFactory loggerFactory)
    : IQuickStorage
{
    private readonly IConnectionMultiplexer _connectionMultiplexer = connectionProvider.Connection;
    private readonly ILogger<QuickStorageService> _logger = loggerFactory.CreateLogger<QuickStorageService>();

    /// <summary>
    /// Сохранить значение по ключу <paramref href="key" />
    /// </summary>
    /// <param name="key">Ключ</param>
    /// <param name="entity">Сущность для сохранения в наборе. Сериализуется в json</param>
    /// <param name="lifetimeInSeconds">Время жизни ключа</param>
    /// <typeparam name="T">Тип данных сущности</typeparam>
    public Task<bool> SetAsync<T>(string key, T entity, int? lifetimeInSeconds = null)
    {
        var json = JsonSerializer.Serialize(entity);
        return SetStringAsync(key, json, lifetimeInSeconds);
    }


    /// <summary>
    /// Получить значение по ключу. Значение десериализуется из json
    /// </summary>
    /// <param name="key">Ключ</param>
    /// <typeparam name="T">Тип данных получаемой сущности</typeparam>
    public async Task<T?> GetAsync<T>(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new Exception("Key is null or empty.");
        }

        var db = _connectionMultiplexer.GetDatabaseSafe();
        var json = await db.StringGetAsync(key).ConfigureAwait(false);
        if (!json.HasValue)
            return default;
        try
        {
            return JsonSerializer.Deserialize<T?>(json);
        }
        catch (JsonException e)
        {
            _logger.LogError(e, e.Message);
            return default;
        }
    }

    /// <summary>
    /// Удалить значение сохранённое по ключу
    /// </summary>
    /// <param name="key">Ключ</param>
    public Task<bool> DeleteAsync(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new Exception("Key is null or empty.");
        }

        var db = _connectionMultiplexer.GetDatabaseSafe();
        return db.KeyDeleteAsync(key);
    }

    private Task<bool> SetStringAsync(string key, RedisValue value, int? lifetimeInSeconds = null)
    {
        if (lifetimeInSeconds.HasValue)
        {
            return ExecuteWithExpirationAsync(tran => tran.StringSetAsync(key, value), key,
                lifetimeInSeconds.Value);
        }

        var db = _connectionMultiplexer.GetDatabaseSafe();
        return db.StringSetAsync(key, value);
    }

    private async Task<T?> ExecuteWithExpirationAsync<T>(Func<ITransaction, Task<T>> func,
        string key, int lifetimeInSeconds
    )
    {
        var db = _connectionMultiplexer.GetDatabaseSafe();
        var transaction = db.CreateTransaction();
        var resultTask = func(transaction);
        var expirationTask = transaction.KeyExpireAsync(key, lifetimeInSeconds.AsTimeSpan());
        var transactionResult = await transaction.ExecuteAsync().ConfigureAwait(false);
        if (!transactionResult)
            return default;
        var result = await resultTask.ConfigureAwait(false);
        var expiration = await expirationTask.ConfigureAwait(false);
        return expiration
            ? result
            : default;
    }
}