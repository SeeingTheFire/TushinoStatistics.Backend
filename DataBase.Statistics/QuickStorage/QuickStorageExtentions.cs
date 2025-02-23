using StackExchange.Redis;

namespace DataBase.Statistics.QuickStorage;

public static class QuickStorageExtentions
{
    public static IDatabase GetDatabaseSafe(this IConnectionMultiplexer connectionMultiplexer) {
        var iterations = 10;
        var db = connectionMultiplexer.GetDatabase();
        while (!connectionMultiplexer.IsConnected && iterations < 10) {
            Thread.Sleep(200);
            iterations++;
            db = connectionMultiplexer.GetDatabase();
        }
        if (db == null) {
            throw new RedisConnectionException(ConnectionFailureType.UnableToConnect, "Can't access to database.");
        }
        return db;
    }
}