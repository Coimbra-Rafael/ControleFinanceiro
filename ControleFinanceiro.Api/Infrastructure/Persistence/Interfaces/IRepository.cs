namespace ControleFinanceiro.Api.Infrastructure.Persistence.Interfaces;

public interface IRepository : IDisposable
{
    Task Add<T>(T entity) where T : class;
    Task Update<T>(T entity) where T : class;
    Task Delete<T>(T entity) where T : class;
    Task DeleteRange<T>(T[] entityArray) where T : class;
}