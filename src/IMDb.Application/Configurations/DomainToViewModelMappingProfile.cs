using AutoMapper;
using IMDb.Application.Requests.Filme;
using IMDb.Application.Responses.Filme;
using IMDb.Application.Responses.Usuario;
using IMDb.Core;
using IMDb.Data.Entidades;

namespace IMDb.Application.Configurations
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Filme, FilmeResponse>();
            CreateMap<Usuario, UsuarioResponse>();
            CreateMap<Pagination<Usuario>, Pagination<UsuarioResponse>>();
            CreateMap<Pagination<Filme>, Pagination<FilmeResponse>>();
            CreateMap<Data.Requests.Filme.ListFilmeRequest, ListFilmeRequest>();
            CreateMap<Data.Responses.Usuario.TokenResponse, TokenResponse>();
        }
    }
}
