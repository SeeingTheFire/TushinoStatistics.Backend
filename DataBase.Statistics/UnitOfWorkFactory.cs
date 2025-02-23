using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataBase.Statistics;

public class UnitOfWorkFactory<TDbContext>(TDbContext? context, ILoggerFactory loggerFactory) : IUnitOfWorkFactory
    where TDbContext : DbContext
{
    private readonly TDbContext? _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<UnitOfWorkFactory<TDbContext>> _logger = loggerFactory.CreateLogger<UnitOfWorkFactory<TDbContext>>();

    public IUnitOfWork Create() {
        return new UnitOfWork(_context, _logger, IsolationLevel.ReadCommitted);
    }

    public IUnitOfWork Create(IsolationLevel isolationLevel) {
        return new UnitOfWork(_context, _logger, isolationLevel);
    }
}