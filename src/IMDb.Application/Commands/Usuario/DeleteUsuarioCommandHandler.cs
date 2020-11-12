using FluentValidation.Results;
using IMDb.Application.Requests.Usuario;
using IMDb.Core;
using IMDb.Data.Interfaces.Repositorios;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IMDb.Application.Commands.Usuario
{
    public class DeleteUsuarioCommandHandler :
        CommandHandler, IRequestHandler<DeleteUsuarioRequest, ValidationResult>
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public DeleteUsuarioCommandHandler(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<ValidationResult> Handle(DeleteUsuarioRequest request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var usuario = await _usuarioRepositorio.RetornarUsuarioPeloId(request.Id);

            if (usuario == null)
            {
                AddError("Não existe nenhum usuário com esse id");
                return ValidationResult;
            }

            usuario.Delete();

            _usuarioRepositorio.Update(usuario);

            return await PersistData(_usuarioRepositorio.UnitOfWork);
        }
    }
}
