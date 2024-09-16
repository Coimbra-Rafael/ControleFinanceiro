using Microsoft.EntityFrameworkCore.Storage;

namespace ControleFinanceiro.Api.Infrastructure.Persistence.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
   Task<IDbContextTransaction> BeginTransaction();
    Task<bool> CommitAsync();
    Task RollbackAsync();
}