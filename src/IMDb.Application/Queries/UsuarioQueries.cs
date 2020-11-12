using AutoMapper;
using IMDb.Application.Interfaces.Queries;
using IMDb.Application.Requests.Usuario;
using IMDb.Application.Responses.Usuario;
using IMDb.Core;
using IMDb.Data.Interfaces.Repositorios;
using System.Threading.Tasks;

namespace IMDb.Application.Queries
{
    public class UsuarioQueries : IUsuarioQueries
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioQueries(
            IMapper mapper,
            IUsuarioRepositorio usuarioRepositorio
        )
        {
            _mapper = mapper;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<TokenResponse> Autenticar(AutenticationUsuarioRequest request)
        {
            return _mapper.Map<TokenResponse>(await _usuarioRepositorio.Autenticar(request.Email, request.Senha));
        }

        public async Task<TokenResponse> RefreshToken(long id)
        {
            return _mapper.Map<TokenResponse>(await _usuarioRepositorio.RefreshToken(id));
        }

        public async Task<Pagination<UsuarioResponse>> RetornarUsuariosNaoAdministrativos(PaginationSearch request)
        {
            return _mapper.Map<Pagination<UsuarioResponse>>(await _usuarioRepositorio.RetornarUsuariosNaoAdministrativos(request));
        }
    }
}
