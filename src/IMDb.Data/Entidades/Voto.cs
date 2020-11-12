using IMDb.Core;
using IMDb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMDb.Data.Entidades
{
    public class Voto : Entity, IAggregateRoot
    {
        protected Voto()
        {

        }

        public Voto(
            long usuarioId, 
            long filmeId,
            int valor
        )
        {
            UsuarioId = usuarioId;
            FilmeId = filmeId;
            Valor = valor;
            DataVotacao = DateTime.Now;
        }

        public void Update(int valor)
        {
            Valor = valor;
            DataVotacao = DateTime.Now;
        }

        public long UsuarioId { get; set; }
        public long FilmeId { get; set; }
        public int Valor { get; set; }
        public DateTime DataVotacao { get; set; }

        public Usuario Usuario { get; set; }
        public Filme Filme { get; set; }
    }
}
