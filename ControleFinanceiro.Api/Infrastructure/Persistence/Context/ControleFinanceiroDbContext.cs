using ControleFinanceiro.Api.Domain.Entities;
using ControleFinanceiro.Api.Domain.Struct;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Api.Infrastructure.Persistence.Context;

public class ControleFinanceiroDbContext : DbContext
{
    public ControleFinanceiroDbContext(DbContextOptions<ControleFinanceiroDbContext> options) : base(options){ }
    public DbSet<Pessoas> Pessoas { get; set; }
    public DbSet<ContasAPagar> ContasAPagar { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       base.OnModelCreating(modelBuilder);
       modelBuilder.Entity<Pessoas>().Property(p => p.Id).HasConversion(
        v => v.value,
        v => new IdCustomizado(v));

       modelBuilder.Entity<ContasAPagar>().Property(p => p.Id).HasConversion(
        v => v.value,
        v => new IdCustomizado(v));
    }
}