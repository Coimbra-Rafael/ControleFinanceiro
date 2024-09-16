using ControleFinanceiro.Api.Infrastructure.Persistence.Context;
using ControleFinanceiro.Api.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Api.Infrastructure.Persistence.Services;

public class Repository : IRepository
{
    private readonly ControleFinanceiroDbContext _context;

    public Repository(ControleFinanceiroDbContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public async Task Add<T>(T entity) where T : class
    {
        await _context.AddAsync(entity);
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task Update<T>(T entity) where T : class
    {
        _context.Update(entity);
    }

    public async Task Delete<T>(T entity) where T : class
    {
        _context.Remove(entity);
    }

    public async Task DeleteRange<T>(T[] entityArray) where T : class
    {
        _context.RemoveRange(entityArray);
    }


    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}