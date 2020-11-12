using IMDb.Core.Interfaces;
using IMDb.Data.Contextos;
using IMDb.Data.Entidades;
using IMDb.Data.Interfaces.Repositorios;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IMDb.Data.Repositorios
{
    public class VotoRepositorio : IVotoRepositorio
    {
        private readonly IMDbContexto _contexto;
        public IUnitOfWork UnitOfWork => _contexto;

        public VotoRepositorio(IMDbContexto contexto)
        {
            _contexto = contexto;
        }
        
        public async Task<int> RetornarSomatoriaVotos(long filmeId)
        {
            return await _contexto.Votos.AsNoTracking().Where(x => x.FilmeId == filmeId).SumAsync(x => x.Valor);
        }

        public async Task<Voto> VerificaDuplicidade(long usuarioId, long filmeId)
        {
            return await _contexto.Votos.AsNoTracking().FirstOrDefaultAsync(x => x.UsuarioId == usuarioId && x.FilmeId == filmeId);
        }

        public void Create(Voto voto)
        {
            _contexto.Votos.Add(voto);
        }

        public void Update(Voto voto)
        {
            _contexto.Votos.Update(voto);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _contexto.Dispose();
        }
    }
}
