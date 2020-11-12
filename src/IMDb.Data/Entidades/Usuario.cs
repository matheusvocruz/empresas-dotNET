using IMDb.Core;
using IMDb.Core.Interfaces;
using System;

namespace IMDb.Data.Entidades
{
    public class Usuario : Entity, IAggregateRoot
    {
        protected Usuario()
        {

        }

        public Usuario(
            string nome,
            string email,
            string senha,
            bool administrador
        )
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Administrador = administrador;
            Excluido = false;
            DataCadastro = DateTime.Now;
        }

        public void Update(
            string nome,
            string email,
            string senha,
            bool administrador
        )
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Administrador = administrador;
        }

        public void Delete()
        {
            Excluido = true;
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Administrador { get; set; }
        public bool Excluido { get; set; }
        public DateTime DataCadastro { get; private set; }
    }
}
