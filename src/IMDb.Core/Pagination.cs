using System.Collections.Generic;
using System.Linq;

namespace IMDb.Core
{
    public class Pagination<TEntity>
    {
        public int Total { get; set; }
        public int Pagina { get; set; }
        public int TotalPaginas { get; set; }
        public IEnumerable<TEntity> Lista { get; set; }

        protected Pagination()
        {
        
        }

        public Pagination(IEnumerable<TEntity> lista, PaginationSearch pesquisa)
        {
            lista = OrdernarLista(lista, pesquisa);

            this.Total = lista.Count();
            this.Lista = lista.Skip((pesquisa.IndiceDePagina - 1) * pesquisa.RegistrosPorPagina).Take(pesquisa.RegistrosPorPagina);
            this.Pagina = pesquisa.IndiceDePagina;

            CalcularTotalPaginas();

            Retornar();
        }

        private IEnumerable<TEntity> OrdernarLista(IEnumerable<TEntity> lista, PaginationSearch pesquisa)
        {
            if (!string.IsNullOrEmpty(pesquisa.Coluna))
            {
                var propertyInfo = typeof(TEntity).GetProperty(pesquisa.Coluna);

                return (pesquisa.Ordenacao.ToLower() == "desc"
                    ? lista.OrderByDescending(x => propertyInfo.GetValue(x, null))
                    : lista.OrderBy(x => propertyInfo.GetValue(x, null)));
            }

            return lista;
        }

        private void CalcularTotalPaginas()
        {
            int resto = this.Total % 10;

            if (resto > 0)
            {
                this.TotalPaginas = (this.Total / 10) + 1;
            }
            else
            {
                this.TotalPaginas = this.Total / 10;
            }
        }

        public Pagination<TEntity> Retornar()
        {
            return this;
        }
    }
}
