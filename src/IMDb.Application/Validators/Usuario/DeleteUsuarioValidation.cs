using FluentValidation;
using IMDb.Application.Requests.Usuario;

namespace IMDb.Application.Validators.Usuario
{
    public class DeleteUsuarioValidation : AbstractValidator<DeleteUsuarioRequest>
    {
        public DeleteUsuarioValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}
