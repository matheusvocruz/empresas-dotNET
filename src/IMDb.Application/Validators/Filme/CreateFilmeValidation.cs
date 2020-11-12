using FluentValidation;
using IMDb.Application.Requests.Filme;

namespace IMDb.Application.Validators.Filme
{
    public class CreateFilmeValidation : AbstractValidator<CreateFilmeRequest>
    {
        public CreateFilmeValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty();

            RuleFor(c => c.Descricao)
                .NotEmpty();

            RuleFor(c => c.Diretor)
                .NotEmpty();

            RuleFor(c => c.Genero)
                .NotEmpty();
        }
    }
}
