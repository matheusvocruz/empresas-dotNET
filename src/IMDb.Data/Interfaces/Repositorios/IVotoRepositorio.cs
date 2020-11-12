using IMDb.Core.Interfaces;
using IMDb.Data.Entidades;
using System.Threading.Tasks;

namespace IMDb.Data.Interfaces.Repositorios
{
    public interface IVotoRepositorio : IRepository<Voto>
    {
        Task<int> RetornarSomatoriaVotos(long filmeId);
        Task<Voto> VerificaDuplicidade(long usuarioId, long filmeId);
        void Create(Voto voto);
        void Update(Voto voto);
    }
}
