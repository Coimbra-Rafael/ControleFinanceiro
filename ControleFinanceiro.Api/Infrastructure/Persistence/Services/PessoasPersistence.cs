using ControleFinanceiro.Api.Domain.Entities;
using ControleFinanceiro.Api.Domain.Struct;
using ControleFinanceiro.Api.Infrastructure.Persistence.Context;
using ControleFinanceiro.Api.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Api.Infrastructure.Persistence.Services;

public class PessoasPersistence : IPessoasPersistence
{
    private readonly ControleFinanceiroDbContext _context;

    public PessoasPersistence(ControleFinanceiroDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pessoas>> BuscaTodasAsPessoas()
    {
        return await _context.Pessoas.ToListAsync();
    }

    public async Task<Pessoas> BuscandoPessoaPorId(IdCustomizado id)
    {
        #pragma warning disable CS8603 // Possible null reference return.
        return await _context.Pessoas.Where(p => p.Id.Equals(id)).FirstOrDefaultAsync();
        #pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<IEnumerable<Pessoas>> BuscandoPessoaPorNome(string nome)
    {
        return await _context.Pessoas.Where(p => p.NomeCompleto.Contains(nome)).ToListAsync();
    }

    public async Task<Pessoas> BuscandoPessoaPorCadastroDePessoasFisica(string cadastroDePessoaFisica)
    {
        #pragma warning disable CS8603 // Possible null reference return.
        return await _context.Pessoas.Where(p => p.CadastroDePessoaFisica.Equals(cadastroDePessoaFisica)).FirstOrDefaultAsync();
        #pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<Pessoas> BuscandoPessoaPorRegistroGeral(string registroGeral)
    {
        #pragma warning disable CS8603 // Possible null reference return.
        return await _context.Pessoas.Where(p => p.RegistroGeral.Equals(registroGeral)).FirstOrDefaultAsync();
        #pragma warning restore CS8603 // Possible null reference return.
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}