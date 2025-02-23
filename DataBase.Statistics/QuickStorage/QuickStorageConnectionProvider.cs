using StackExchange.Redis;

namespace DataBase.Statistics.QuickStorage;

public sealed class QuickStorageConnectionProvider : IDisposable {
    public IConnectionMultiplexer Connection { get; }


    public QuickStorageConnectionProvider(string connectionString) {
        if (Connection == null) {
            Connection = ConnectionMultiplexer.Connect(connectionString);
        }
    }

    public void Dispose() {
        Connection?.Dispose();
    }
}