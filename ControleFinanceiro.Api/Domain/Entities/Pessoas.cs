using ControleFinanceiro.Api.Domain.Abstractions;

namespace ControleFinanceiro.Api.Domain.Entities;

public class Pessoas : Entity, IDisposable
{
    public string NomeCompleto { get; set; } = null!;
    public string RegistroGeral { get; set; } = null!;
    public string CadastroDePessoaFisica { get; set; } = null!;
    public DateTime DataDeNascimento { get; set; }
    public decimal? ValorEmConta { get; set; }
    public decimal? ValorInvestido { get; set; }
    public DateTime? UpdateOn { get; set; }
    
    public Pessoas(){    }

    public Pessoas(string nomeCompleto, string registroGeral, string cadastroDePessoaFisica, decimal? valorEmConta, decimal? valorInvestido, DateTime dataDeNascimento, DateTime? updateOn = null)
    {
        NomeCompleto = nomeCompleto;
        RegistroGeral = registroGeral;
        CadastroDePessoaFisica = cadastroDePessoaFisica;
        ValorEmConta = valorEmConta;
        ValorInvestido = valorInvestido;
        DataDeNascimento = dataDeNascimento;
        UpdateOn = updateOn;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}