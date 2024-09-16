using ControleFinanceiro.Api.Application.DataTransferObject;
using ControleFinanceiro.Api.Application.Interfaces;
using ControleFinanceiro.Api.Application.Mapper.Services;
using ControleFinanceiro.Api.Infrastructure.Persistence.Interfaces;
using ControleFinanceiro.Api.Infrastructure.Persistence.UnitOfWork;

namespace ControleFinanceiro.Api.Application.Services;

public class PessoasServices : IPessoasServices
{
    private readonly IPessoasPersistence _persistence;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository _repository;


    public PessoasServices(IPessoasPersistence persistence, IUnitOfWork unitOfWork, IRepository repository)
    {
        _persistence = persistence;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    #pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    public async Task<IEnumerable<PessoasDTO>?> BuscaTodasAsPessoas()
    {
        try
        {
            var pessoas = await _persistence.BuscaTodasAsPessoas();

            if (pessoas.Count() > 0)
            {   
                using (var mapperPessoa = new MapperPessoas())
                {
                    return mapperPessoa.MapperListaDePessoasParaListaDePessoasDto(pessoas);
                }
                
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public Task<PessoasDTO> BuscandoPessoaPorId(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PessoasDTO>> BuscandoPessoaPorNome(string nome)
    {
        throw new NotImplementedException();
    }

    public Task<PessoasDTO> BuscandoPessoaPorRegistroGeral(string registroGeral)
    {
        throw new NotImplementedException();
    }

    public Task<PessoasDTO> BuscandoPessoaPorCadastroDePessoasFisica(string cadastroDePessoaFisica)
    {
        throw new NotImplementedException();
    }

    public async Task<PessoasDTO?> AdicionaPessoa(PessoasDTO model)
    {
        var mapper = new MapperPessoas();
        try
        {
            var pessoa = mapper.MapperPessoasDtoParaPessoas(model);
            await _unitOfWork.BeginTransaction();
            
            if(string.IsNullOrEmpty(pessoa.NomeCompleto))
            {
                throw new Exception("Por favor preencha o campo nome!");
            }

            if(string.IsNullOrEmpty(pessoa.RegistroGeral))
            {
                throw new Exception("Por favor preencha o campo RG!");
            }

            if(string.IsNullOrEmpty(pessoa.CadastroDePessoaFisica))
            {
                throw new Exception("Por favor preencha o campo CPF!");
            }

            if(!pessoa.ValorEmConta.HasValue)
            {
                throw new Exception("Por favor preencha o campo de valor em conta");
            }

            if(!pessoa.ValorInvestido.HasValue)
            {
                throw new Exception("Por favor preencha o campo de valor investido");
            }
            await _repository.Add(pessoa);
            var result = await _unitOfWork.CommitAsync();

            if(result) return mapper.MapperPessoasParaPessoasDto(pessoa);

            return null;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public Task<PessoasDTO> AtualizaPessoa(PessoasDTO model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> excluiPessoa(string id)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _persistence.Dispose();
        _unitOfWork.Dispose();
        GC.SuppressFinalize(this);
    }
}