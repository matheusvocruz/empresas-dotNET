using AutoMapper;
using IMDb.Application.Interfaces.Queries;
using IMDb.Application.Requests.Filme;
using IMDb.Application.Responses.Filme;
using IMDb.Core;
using IMDb.Data.Interfaces.Repositorios;
using System.Threading.Tasks;

namespace IMDb.Application.Queries
{
    public class FilmeQueries : IFilmeQueries
    {
        private readonly IMapper _mapper;
        private readonly IFilmeRepositorio _filmeRepositorio;

        public FilmeQueries(
            IMapper mapper,
            IFilmeRepositorio filmeRepositorio
        )
        {
            _mapper = mapper;
            _filmeRepositorio = filmeRepositorio;
        }

        public async Task<FilmeResponse> RetornarFilmePeloId(long id)
        {
            return _mapper.Map<FilmeResponse>(await _filmeRepositorio.RetornarFilmePeloId(id));
        }

        public async Task<Pagination<FilmeResponse>> RetornarFilmes(ListFilmeRequest request)
        {
            var newRequest = new Data.Requests.Filme.ListFilmeRequest {
                Nome = request.Nome,
                Diretor = request.Diretor,
                Coluna = request.Coluna,
                Genero = request.Genero,
                IndiceDePagina = request.IndiceDePagina,
                Ordenacao = request.Ordenacao,
                RegistrosPorPagina = request.RegistrosPorPagina
            };

            return _mapper.Map<Pagination<FilmeResponse>>(await _filmeRepositorio.RetornarFilmes(newRequest));
        }
    }
}
