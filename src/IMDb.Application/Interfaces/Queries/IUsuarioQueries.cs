using IMDb.Application.Requests.Usuario;
using IMDb.Application.Responses.Usuario;
using IMDb.Core;
using System.Threading.Tasks;

namespace IMDb.Application.Interfaces.Queries
{
    public interface IUsuarioQueries
    {
        Task<TokenResponse> Autenticar(AutenticationUsuarioRequest request);
        Task<TokenResponse> RefreshToken(long id);
        Task<Pagination<UsuarioResponse>> RetornarUsuariosNaoAdministrativos(PaginationSearch request);
    }
}
