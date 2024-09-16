using ControleFinanceiro.Api.Application.DataTransferObject;

namespace ControleFinanceiro.Api.Application.Interfaces
{
    public interface IPessoasServices : IDisposable
    {
        Task<IEnumerable<PessoasDTO>> BuscaTodasAsPessoas();
        Task<PessoasDTO> BuscandoPessoaPorId(string id);
        Task<IEnumerable<PessoasDTO>> BuscandoPessoaPorNome(string nome);
        Task<PessoasDTO> BuscandoPessoaPorRegistroGeral(string registroGeral);
        Task<PessoasDTO> BuscandoPessoaPorCadastroDePessoasFisica(string cadastroDePessoaFisica);
        Task<PessoasDTO> AdicionaPessoa(PessoasDTO model);
        Task<PessoasDTO> AtualizaPessoa(PessoasDTO model, string id);
        Task<bool> excluirPessoa(string id);
    }
}