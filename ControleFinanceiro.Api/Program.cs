using ControleFinanceiro.Api.Application.Interfaces;
using ControleFinanceiro.Api.Application.Services;
using ControleFinanceiro.Api.Infrastructure.Persistence.Context;
using ControleFinanceiro.Api.Infrastructure.Persistence.Interfaces;
using ControleFinanceiro.Api.Infrastructure.Persistence.Services;
using ControleFinanceiro.Api.Infrastructure.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ControleFinanceiroDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgreSql"))
);
// Infrastructure
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IPessoasPersistence, PessoasPersistence>();

// Application
builder.Services.AddScoped<IPessoasServices, PessoasServices>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();