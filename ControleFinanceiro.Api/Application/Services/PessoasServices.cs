using ControleFinanceiro.Api.Application.DataTransferObject;
using ControleFinanceiro.Api.Application.Interfaces;
using ControleFinanceiro.Api.Application.Mapper.Services;
using ControleFinanceiro.Api.Domain.Struct;
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

    public async Task<PessoasDTO> BuscandoPessoaPorId(string id)
    {
        try
        {
            var pessoa = await _persistence.BuscandoPessoaPorId(new IdCustomizado(Guid.Parse(id)));
            if(pessoa.Equals(null)) throw new Exception("Não foi possível localizar o id na base!");
            using (var mapper = new MapperPessoas())
            {
                return mapper.MapperPessoasParaPessoasDto(pessoa);
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<PessoasDTO>?> BuscandoPessoaPorNome(string nome)
    {
        try
        {
            var pessoas = await _persistence.BuscandoPessoaPorNome(nome);

            if(pessoas.Count() > 0)
            {
                using (var mapper = new MapperPessoas())
                {
                    return mapper.MapperListaDePessoasParaListaDePessoasDto(pessoas);
                }
            }
            return null;
        
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<PessoasDTO> BuscandoPessoaPorRegistroGeral(string registroGeral)
    {
        try
        {
            var pessoa = await _persistence.BuscandoPessoaPorRegistroGeral(registroGeral);

            using (var mapper = new MapperPessoas())
            {
                return mapper.MapperPessoasParaPessoasDto(pessoa);   
            }
        }
        catch (Exception ex)
        {
            
            throw new Exception(ex.Message);
        }
    }

    public async Task<PessoasDTO> BuscandoPessoaPorCadastroDePessoasFisica(string cadastroDePessoaFisica)
    {
        
        try
        {
            var pessoa = await _persistence.BuscandoPessoaPorCadastroDePessoasFisica(cadastroDePessoaFisica);

            using (var mapper = new MapperPessoas())
            {
                return mapper.MapperPessoasParaPessoasDto(pessoa);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<PessoasDTO?> AdicionaPessoa(PessoasDTO model)
    {
        using var mapper = new MapperPessoas();
        try
        {
            using (var pessoa = mapper.MapperPessoasDtoParaPessoas(model))
            {
                await _unitOfWork.BeginTransaction();

                if (string.IsNullOrEmpty(pessoa.NomeCompleto))
                {
                    throw new Exception("Por favor preencha o campo nome!");
                }

                if (string.IsNullOrEmpty(pessoa.RegistroGeral))
                {
                    throw new Exception("Por favor preencha o campo RG!");
                }

                if (string.IsNullOrEmpty(pessoa.CadastroDePessoaFisica))
                {
                    throw new Exception("Por favor preencha o campo CPF!");
                }

                if (!pessoa.ValorEmConta.HasValue)
                {
                    throw new Exception("Por favor preencha o campo de valor em conta");
                }

                if (!pessoa.ValorInvestido.HasValue)
                {
                    throw new Exception("Por favor preencha o campo de valor investido");
                }
                await _repository.Add(pessoa);
                var result = await _unitOfWork.CommitAsync();
                
                if (result) return mapper.MapperPessoasParaPessoasDto(pessoa);

                return null;
            }
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
        finally
        {
            mapper.Dispose();
            _repository.Dispose();
            _unitOfWork.Dispose();
        }
    }

    public async Task<PessoasDTO> AtualizaPessoa(PessoasDTO model)
    {
        using var mapper = new MapperPessoas();
        try{
        var pessoa = await _persistence.BuscandoPessoaPorId(new IdCustomizado(Guid.Parse(model.id)));

        await _unitOfWork.BeginTransaction();

        if (pessoa.Equals(null)) throw new Exception("Pessoa não encontrada na base para ser atualizada!");

        await _repository.Update(mapper.MapperPessoasDtoParaPessoas(model));

        await _unitOfWork.CommitAsync();
        return mapper.MapperPessoasParaPessoasDto(await _persistence.BuscandoPessoaPorId(new IdCustomizado(Guid.Parse(model.id))));
        }
        catch(Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
        finally
        {
            mapper.Dispose();
            _repository.Dispose();
            _unitOfWork.Dispose();
        }
    }

    public async Task<bool> excluirPessoa(string id)
    {
        try
        {
            var pessoa = await _persistence.BuscandoPessoaPorId(new IdCustomizado(Guid.Parse(id)));

            await _unitOfWork.BeginTransaction();
            if(pessoa.Equals(null)) throw new Exception("Não foi possível localizar pessoa na base");

            await _repository.Delete(pessoa);
            return await _unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
        finally
        {
            _persistence.Dispose();
            _repository.Dispose();
            _unitOfWork.Dispose();
        }
    }

    public void Dispose()
    {
        _repository.Dispose();
        _persistence.Dispose();
        _unitOfWork.Dispose();
        GC.SuppressFinalize(this);
    }
}