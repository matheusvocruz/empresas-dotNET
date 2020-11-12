using System;
using System.Collections.Generic;
using System.Text;

namespace IMDb.Core
{
    public class PaginationSearch
    {
        public PaginationSearch()
        {
            this.IndiceDePagina = 1;
            this.RegistrosPorPagina = 10;
            this.Ordenacao = "asc";
        }

        public int IndiceDePagina { get; set; }
        public int RegistrosPorPagina { get; set; }
        public string Coluna { get; set; }
        public string Ordenacao { get; set; }
    }
}
