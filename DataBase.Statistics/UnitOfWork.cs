using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataBase.Statistics;

internal class UnitOfWork(DbContext? context, ILogger logger, IsolationLevel isolationLevel) : IUnitOfWork
{
    private TransactionScope? _currentTransaction = new(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = isolationLevel }, TransactionScopeAsyncFlowOption.Enabled);
    private DbContext? _context = context;

    public async Task<bool> SaveChangesAsync() {
        if (_currentTransaction == null)
            return false;

        await _context?.SaveChangesAsync()!;

        return true;
    }

    public bool Complete() {
        _currentTransaction?.Complete();
        try {
            _currentTransaction?.Dispose();
        } catch (Exception ex) {
            logger.LogError(ex, ex.Message);
            return false;
        } finally {
            _currentTransaction = null;
            _context = null;
        }

        return true;
    }

    public void Dispose() {
        _currentTransaction?.Dispose();
        _currentTransaction = null;
        _context = null;
    }
}