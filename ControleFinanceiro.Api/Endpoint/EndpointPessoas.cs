using ControleFinanceiro.Api.Application.DataTransferObject;
using ControleFinanceiro.Api.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Api.Endpoint;

public static class EndpointPessoas
{
    public static void MapEndpointPessoas(this WebApplication app)
    {
        var group = app.MapGroup("/v1");

        group.MapGet("/Pessoas", BuscaTodasAsPessoas).WithName("Buscar todas as pessoas").WithOpenApi();
        group.MapGet("/Pessoas/id", BuscarPessoaPorId).WithName("Buscar pessoa por Id").WithOpenApi();
        group.MapPost("/Pessoas",AdicionarPessoa).WithName("Cadastrar pessoa").WithOpenApi();
        group.MapPut("/Pessoas", AtualizarPessoa).WithName("Atualizar cadastro de pessoa").WithOpenApi();
        group.MapDelete("/Pessoas", ExcluirPessoa).WithName("Excluir cadastro de pessoa").WithOpenApi();

    }

    private static async Task<IResult> BuscaTodasAsPessoas([FromServices] IPessoasServices service) 
    {
        return Results.Ok(await service.BuscaTodasAsPessoas());
    }
    private static async Task<IResult> BuscarPessoaPorId([FromServices] IPessoasServices service, [FromHeader] string id) 
    {
        return Results.Ok(await service.BuscandoPessoaPorId(id));
    }
    private static async Task<IResult> AdicionarPessoa([FromServices] IPessoasServices services, [FromBody] PessoasDTO pessoasDTO)
    {
        return Results.Ok(await services.AdicionaPessoa(pessoasDTO));
    }

    private static async Task<IResult> AtualizarPessoa([FromServices] IPessoasServices services, [FromHeader] string id, [FromBody] PessoasDTO pessoasDTO)
    {
        return Results.Ok(await services.AtualizaPessoa(pessoasDTO, id));
    }

    private static async Task<IResult> ExcluirPessoa([FromServices] IPessoasServices services, [FromHeader] string id)
    {
        return Results.Ok(await services.excluirPessoa(id));
    }
}