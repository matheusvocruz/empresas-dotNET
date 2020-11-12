using FluentValidation.Results;
using IMDb.Application.Requests.Usuario;
using IMDb.Core;
using IMDb.Data.Interfaces.Repositorios;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IMDb.Application.Commands.Usuario
{
    public class UpdateUsuarioCommandHandler :
        CommandHandler, IRequestHandler<UpdateUsuarioRequest, ValidationResult>
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UpdateUsuarioCommandHandler(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<ValidationResult> Handle(UpdateUsuarioRequest request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var duplicidadeUsuario = await _usuarioRepositorio.RetornarUsuarioDuplicadoPorEmail(request.Email, request.Id);

            if (duplicidadeUsuario != null)
            {
                AddError("Já existe um usuário com esse email");
                return ValidationResult;
            }

            var usuario = await _usuarioRepositorio.RetornarUsuarioPeloId(request.Id);

            if (usuario == null)
            {
                AddError("Não existe nenhum usuário com esse id");
                return ValidationResult;
            }

            usuario.Update(request.Nome, request.Email, _usuarioRepositorio.HashSenha(request.Senha), request.Administrador);

            _usuarioRepositorio.Update(usuario);

            return await PersistData(_usuarioRepositorio.UnitOfWork);
        }
    }
}
