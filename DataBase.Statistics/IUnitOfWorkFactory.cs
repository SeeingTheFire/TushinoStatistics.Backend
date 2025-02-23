using System.Transactions;

namespace DataBase.Statistics;

public interface IUnitOfWorkFactory {
    IUnitOfWork Create();

    IUnitOfWork Create(IsolationLevel isolationLevel);
}