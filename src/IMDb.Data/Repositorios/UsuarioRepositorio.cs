using IMDb.Core;
using IMDb.Core.Extensions;
using IMDb.Core.Interfaces;
using IMDb.Data.Contextos;
using IMDb.Data.Entidades;
using IMDb.Data.Interfaces.Repositorios;
using IMDb.Data.Responses.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IMDb.Data.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly IMDbContexto _contexto;
        private readonly AppSettings _appSettings;
        public IUnitOfWork UnitOfWork => _contexto;

        public UsuarioRepositorio(
            IMDbContexto contexto, 
            AppSettings appSettings
        )
        {
            _contexto = contexto;
            _appSettings = appSettings;
        }

        public async Task<Usuario> RetornarUsuarioPeloId(long id)
        {
            return await _contexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Usuario> RetornarUsuarioPorEmail(string email)
        {
            return await _contexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        }

        private async Task<Usuario> RetornarUsuarioPorEmailESenha(string email, string senha)
        {
            return await _contexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email && x.Senha == this.HashSenha(senha));
        }

        public async Task<Usuario> RetornarUsuarioDuplicadoPorEmail(string email, long id)
        {
            return await _contexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email && x.Id != id);
        }

        public async Task<Pagination<Usuario>> RetornarUsuariosNaoAdministrativos(PaginationSearch request)
        {
            var lista =  await _contexto.Usuarios.AsNoTracking().Where(x => !x.Administrador && !x.Excluido).OrderBy(x => x.Nome).ToListAsync();

            return new Pagination<Usuario>(lista, request);
        }

        public async Task<TokenResponse> Autenticar(string email, string senha)
        {
            var usuario = await this.RetornarUsuarioPorEmailESenha(email, senha);

            if(usuario == null)
            {
                return null;
            }

            return this.GerarToken(usuario);
        }

        private TokenResponse GerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Email.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim("TipoUsuario", (usuario.Administrador ? "Administrador" : "Usuario"))
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenResponse()
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = tokenDescriptor.Expires.Value.AddHours((_appSettings.Lifetime * -1))
            };
        }

        public async Task<TokenResponse> RefreshToken(long id)
        {
            var usuario = await this.RetornarUsuarioPeloId(id);

            if (usuario == null)
            {
                return null;
            }

            return this.GerarToken(usuario);
        }

        public string HashSenha(string value)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] hashBytes;
            using (HashAlgorithm hash = SHA1.Create())
                hashBytes = hash.ComputeHash(encoding.GetBytes(value));

            StringBuilder hashValue = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes)
            {
                hashValue.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);
            }

            return hashValue.ToString();
        }

        public void Create(Usuario usuario)
        {
            _contexto.Usuarios.Add(usuario);
        }

        public void Update(Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
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
