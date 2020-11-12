using IMDb.Core;

namespace IMDb.Application.Requests.Filme
{
    public class ListFilmeRequest : PaginationSearch
    {
        public string Nome { get; set; }
        public string Diretor { get; set; }
        public string Genero { get; set; }
    }
}
