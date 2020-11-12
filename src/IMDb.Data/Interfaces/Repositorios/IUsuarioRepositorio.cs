using IMDb.Core;
using IMDb.Core.Interfaces;
using IMDb.Data.Entidades;
using IMDb.Data.Responses.Usuario;
using System.Threading.Tasks;

namespace IMDb.Data.Interfaces.Repositorios
{
    public interface IUsuarioRepositorio : IRepository<Usuario>
    {
        Task<Usuario> RetornarUsuarioPeloId(long id);
        Task<Usuario> RetornarUsuarioPorEmail(string email);
        Task<Usuario> RetornarUsuarioDuplicadoPorEmail(string email, long id);
        Task<Pagination<Usuario>> RetornarUsuariosNaoAdministrativos(PaginationSearch request);
        Task<TokenResponse> Autenticar(string email, string senha);
        Task<TokenResponse> RefreshToken(long id);
        string HashSenha(string value);
        void Create(Usuario usuario);
        void Update(Usuario usuario);
    }
}
