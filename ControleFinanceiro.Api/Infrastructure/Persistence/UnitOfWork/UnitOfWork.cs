using ControleFinanceiro.Api.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ControleFinanceiro.Api.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ControleFinanceiroDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(ControleFinanceiroDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IDbContextTransaction> BeginTransaction()
    {
        if (!await _context.Database.CanConnectAsync())
        {
            await _context.Database.OpenConnectionAsync();
        }

        if (_transaction != null)
        {
            throw new InvalidOperationException("Uma transação já está em andamento.");
        }

        _transaction = await _context.Database.BeginTransactionAsync();
        return _transaction;
    }

    public async Task<bool> CommitAsync()
    {
        try
        {
            int rowsAffected = await _context.SaveChangesAsync();
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
            return rowsAffected > 0;
        }
        catch (Exception)
        {
            _transaction?.Dispose();
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context?.Dispose();
    }
}