using FluentValidation;
using IMDb.Application.Requests.Voto;

namespace IMDb.Application.Validators.Voto
{
    public class CreateVotoValidation : AbstractValidator<CreateVotoRequest>
    {
        public CreateVotoValidation()
        {
            RuleFor(c => c.FilmeId)
                .NotEmpty();

            RuleFor(c => c.Valor)
                .NotEmpty()
                .LessThanOrEqualTo(5)
                .GreaterThanOrEqualTo(1);
        }
    }
}
