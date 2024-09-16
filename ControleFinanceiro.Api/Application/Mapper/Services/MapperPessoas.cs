using ControleFinanceiro.Api.Application.DataTransferObject;
using ControleFinanceiro.Api.Application.Mapper.Interfaces;
using ControleFinanceiro.Api.Domain.Entities;

namespace ControleFinanceiro.Api.Application.Mapper.Services;

public class MapperPessoas : IMapperPessoas
{

    public IEnumerable<Pessoas> MapperListaDePessoasDtoParaListaDePesssoas(IEnumerable<PessoasDTO> listaPessoasDto)
    {
        var listaPessoas = new List<Pessoas>();
        foreach(var pessoaDto in listaPessoasDto)
        {
            var pessoa = new Pessoas
            (
                pessoaDto.nomeCompleto,
                pessoaDto.registroGeral,
                pessoaDto.CadastroDePessoaFisica,
                pessoaDto.ValorEmConta,
                pessoaDto.ValorInvestido,
                pessoaDto.DataDeNascimento
            );
            listaPessoas.Add(pessoa);
        }
        return listaPessoas;
    }

    public IEnumerable<PessoasDTO> MapperListaDePessoasParaListaDePessoasDto(IEnumerable<Pessoas> listaPessoas)
    {
        var listaPessoasDto = new List<PessoasDTO>();
        foreach(var pessoa in listaPessoas)
        {
            var pessoaDto = new PessoasDTO
            (
                pessoa.Id.ToString(),
                pessoa.NomeCompleto,
                pessoa.RegistroGeral,
                pessoa.CadastroDePessoaFisica,
                pessoa.DataDeNascimento,
                pessoa.ValorEmConta,
                pessoa.ValorInvestido
            );
            listaPessoasDto.Add(pessoaDto);
        }
        return listaPessoasDto;
    }

    public Pessoas MapperPessoasDtoParaPessoas(PessoasDTO pessoaDto)
    {
        return new Pessoas
            (
                pessoaDto.nomeCompleto,
                pessoaDto.registroGeral,
                pessoaDto.CadastroDePessoaFisica,
                pessoaDto.ValorEmConta,
                pessoaDto.ValorInvestido,
                pessoaDto.DataDeNascimento
            );
    }

    public PessoasDTO MapperPessoasParaPessoasDto(Pessoas pessoa)
    {
        return new PessoasDTO
            (
                pessoa.Id.ToString(),
                pessoa.NomeCompleto,
                pessoa.RegistroGeral,
                pessoa.CadastroDePessoaFisica,
                pessoa.DataDeNascimento,
                pessoa.ValorEmConta,
                pessoa.ValorInvestido
            );
    }

    public void Dispose()
    {
       GC.SuppressFinalize(this);
    }
}