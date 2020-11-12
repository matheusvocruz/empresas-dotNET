using FluentValidation;
using IMDb.Application.Requests.Usuario;

namespace IMDb.Application.Validators.Usuario
{
    public class UpdateUsuarioValidation : AbstractValidator<UpdateUsuarioRequest>
    {
        public UpdateUsuarioValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty();

            RuleFor(c => c.Nome)
                .NotEmpty();

            RuleFor(c => c.Email)
                .NotEmpty();

            RuleFor(c => c.Senha)
                .NotEmpty();

            RuleFor(c => c.Administrador)
                .NotEmpty();
        }
    }
}
