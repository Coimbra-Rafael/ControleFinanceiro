using System.ComponentModel.DataAnnotations;
using ControleFinanceiro.Api.Domain.Struct;

namespace ControleFinanceiro.Api.Domain.Abstractions;

public abstract class Entity
{
    [Key]
    public IdCustomizado Id { get; private set; }
    
    public DateTime CreatedOn { get; private set; }

    protected Entity()
    {
        Id = IdCustomizado.NewGuidCustomerId();
        CreatedOn = DateTime.UtcNow;
    }

    protected Entity(IdCustomizado id)
    {
        Id = id;
        CreatedOn = DateTime.UtcNow;
    }
}