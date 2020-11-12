using FluentValidation;
using IMDb.Application.Requests.Usuario;

namespace IMDb.Application.Validators.Usuario
{
    public class CreateUsuarioValidation : AbstractValidator<CreateUsuarioRequest>
    {
        public CreateUsuarioValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty();

            RuleFor(c => c.Email)
                .NotEmpty();

            RuleFor(c => c.Senha)
                .NotEmpty();
        }
    }
}
