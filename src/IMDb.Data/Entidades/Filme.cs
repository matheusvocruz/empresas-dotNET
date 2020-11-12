using IMDb.Core;
using IMDb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMDb.Data.Entidades
{
    public class Filme : Entity, IAggregateRoot
    {
        protected Filme() { }

        public Filme(
            string nome, 
            string descricao, 
            string diretor, 
            string genero
        )
        {
            Nome = nome;
            Descricao = descricao;
            Diretor = diretor;
            Genero = genero;
        }

        public void Update(
            string nome,
            string descricao,
            string diretor,
            string genero
        )
        {
            Nome = nome;
            Descricao = descricao;
            Diretor = diretor;
            Genero = genero;
        }

        public void AdicionarVoto()
        {
            QuantidadeDeVotos++;
        }

        public void AtualizarMedia(int somatoria)
        {
            Media = (somatoria / Convert.ToDecimal(QuantidadeDeVotos));
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Diretor { get; set; }
        public decimal Media { get; set; }
        public int QuantidadeDeVotos { get; set; }
        public string Genero { get; set; }
    }
}
