using IMDb.Core;
using IMDb.Core.Interfaces;
using IMDb.Data.Entidades;
using IMDb.Data.Requests.Filme;
using System.Threading.Tasks;

namespace IMDb.Data.Interfaces.Repositorios
{
    public interface IFilmeRepositorio : IRepository<Filme>
    {
        Task<Pagination<Filme>> RetornarFilmes(ListFilmeRequest request);
        Task<Filme> RetornarFilmePeloNome(string nome);
        Task<Filme> RetornarFilmePeloId(long id);
        void Create(Filme filme);
        void Update(Filme filme);
    }
}
