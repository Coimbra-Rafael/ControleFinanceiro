using ControleFinanceiro.Api.Domain.Entities;
using ControleFinanceiro.Api.Domain.Struct;

namespace ControleFinanceiro.Api.Infrastructure.Persistence.Interfaces;

public interface IPessoasPersistence : IDisposable
{
    Task<IEnumerable<Pessoas>> BuscaTodasAsPessoas();
    Task<Pessoas> BuscandoPessoaPorId(IdCustomizado id);
    Task<IEnumerable<Pessoas>> BuscandoPessoaPorNome(string nome);
    Task<Pessoas> BuscandoPessoaPorRegistroGeral(string registroGeral);
    Task<Pessoas> BuscandoPessoaPorCadastroDePessoasFisica(string cadastroDePessoaFisica);
}