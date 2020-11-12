namespace IMDb.Application.Responses.Filme
{
    public class FilmeResponse
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Diretor { get; set; }
        public decimal? Media { get; set; }
        public int QuantidadeDeVotos { get; set; }
        public string Genero { get; set; }
    }
}
