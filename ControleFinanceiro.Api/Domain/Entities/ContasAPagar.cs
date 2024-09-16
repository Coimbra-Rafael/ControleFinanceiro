using ControleFinanceiro.Api.Domain.Abstractions;
using ControleFinanceiro.Api.Domain.Struct;

namespace ControleFinanceiro.Api.Domain.Entities;

public class ContasAPagar : Entity, IDisposable
{
    public string? Descricao { get; set; }
    public Int16 QuantidadeDeParcelas { get; set; }
    public decimal ValorAPagar { get; set; }
    public new IdCustomizado Id { get; set; }
    public required Pessoas Pessoas { get; set; }
    public DateOnly UpdateOn { get; set; }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}