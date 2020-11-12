using IMDb.Core;
using IMDb.Core.Interfaces;
using IMDb.Data.Contextos;
using IMDb.Data.Entidades;
using IMDb.Data.Interfaces.Repositorios;
using IMDb.Data.Requests.Filme;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IMDb.Data.Repositorios
{
    public class FilmeRepositorio : IFilmeRepositorio
    {
        private readonly IMDbContexto _contexto;
        public IUnitOfWork UnitOfWork => _contexto;

        public FilmeRepositorio(IMDbContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<Pagination<Filme>> RetornarFilmes(ListFilmeRequest request)
        {
            var lista = this.FiltrarFilmes(_contexto.Filmes.AsNoTracking(), request);

            return new Pagination<Filme>(await lista.ToListAsync(), request);
        }

        private IQueryable<Filme> FiltrarFilmes(IQueryable<Filme> filmes, ListFilmeRequest request)
        {
            if (!string.IsNullOrEmpty(request.Nome))
            {
                filmes = filmes.Where(x => x.Nome.Trim().ToLower().Contains(request.Nome.Trim().ToLower()));
            }

            if (!string.IsNullOrEmpty(request.Diretor))
            {
                filmes = filmes.Where(x => x.Diretor.Trim().ToLower().Contains(request.Diretor.Trim().ToLower()));
            }

            if (!string.IsNullOrEmpty(request.Genero))
            {
                filmes = filmes.Where(x => x.Genero.Trim().ToLower().Contains(request.Genero.Trim().ToLower()));
            }

            return filmes.AsQueryable<Filme>().OrderByDescending(x => x.Media).ThenBy(x => x.Nome);
        }

        public async Task<Filme> RetornarFilmePeloNome(string nome)
        {
            return await _contexto.Filmes.AsNoTracking().FirstOrDefaultAsync(x => x.Nome == nome);
        }

        public async Task<Filme> RetornarFilmePeloId(long id)
        {
            return await _contexto.Filmes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Create(Filme filme)
        {
            _contexto.Filmes.Add(filme);
        }

        public void Update(Filme filme)
        {
            _contexto.Filmes.Update(filme);
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
