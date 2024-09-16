namespace ControleFinanceiro.Api.Application.DataTransferObject;

public record PessoasDTO(string id, string nomeCompleto, string registroGeral, string CadastroDePessoaFisica, DateTime DataDeNascimento, decimal? ValorEmConta, decimal? ValorInvestido)
{
    
}