using ControleFinanceiro.Api.Domain.Struct;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ControleFinanceiro.Api.Infrastructure.Persistence.Context.ConversorTiposCustomizados;

public class IdCustomizadoConverter : ValueConverter<IdCustomizado, Guid>
{
    public IdCustomizadoConverter() : base(v => v.value, v => new IdCustomizado(v))
    {
    }
}