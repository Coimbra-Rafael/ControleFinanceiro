using ControleFinanceiro.Api.Application.DataTransferObject;
using ControleFinanceiro.Api.Domain.Entities;

namespace ControleFinanceiro.Api.Application.Mapper.Interfaces;

public interface IMapperPessoas : IDisposable
{
    Pessoas MapperPessoasDtoParaPessoas(PessoasDTO pessoaDto);
    PessoasDTO MapperPessoasParaPessoasDto(Pessoas pessoa);

    IEnumerable<Pessoas> MapperListaDePessoasDtoParaListaDePesssoas(IEnumerable<PessoasDTO> listaPessoasDto);
    IEnumerable<PessoasDTO> MapperListaDePessoasParaListaDePessoasDto(IEnumerable<Pessoas> listaPessoas);
}