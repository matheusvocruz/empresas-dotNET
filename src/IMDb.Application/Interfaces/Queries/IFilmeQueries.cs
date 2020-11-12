using IMDb.Application.Requests.Filme;
using IMDb.Application.Responses.Filme;
using IMDb.Core;
using System.Threading.Tasks;

namespace IMDb.Application.Interfaces.Queries
{
    public interface IFilmeQueries
    {
        Task<FilmeResponse> RetornarFilmePeloId(long id);
        Task<Pagination<FilmeResponse>> RetornarFilmes(ListFilmeRequest request);
    }
}
