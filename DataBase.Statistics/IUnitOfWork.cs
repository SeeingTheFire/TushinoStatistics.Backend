namespace DataBase.Statistics;

public interface IUnitOfWork : IDisposable {
    Task<bool> SaveChangesAsync();

    bool Complete();
}