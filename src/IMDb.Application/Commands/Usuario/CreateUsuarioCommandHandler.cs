using FluentValidation.Results;
using IMDb.Application.Requests.Usuario;
using IMDb.Core;
using IMDb.Data.Interfaces.Repositorios;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IMDb.Application.Commands.Usuario
{
    public class CreateUsuarioCommandHandler :
        CommandHandler, IRequestHandler<CreateUsuarioRequest, ValidationResult>
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public CreateUsuarioCommandHandler(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<ValidationResult> Handle(CreateUsuarioRequest request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var verificaDuplicidade = await _usuarioRepositorio.RetornarUsuarioPorEmail(request.Email);

            if (verificaDuplicidade != null)
            {
                AddError("Já existe um usuário com esse email");
                return ValidationResult;
            }

            var usuario = new Data.Entidades.Usuario(request.Nome, request.Email, _usuarioRepositorio.HashSenha(request.Senha), request.Administrador);

            _usuarioRepositorio.Create(usuario);

            return await PersistData(_usuarioRepositorio.UnitOfWork);
        }
    }
}
