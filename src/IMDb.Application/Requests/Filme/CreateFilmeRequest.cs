using FluentValidation.Results;
using IMDb.Application.Validators.Filme;
using IMDb.Core;

namespace IMDb.Application.Requests.Filme
{
    public class CreateFilmeRequest : Command
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Diretor { get; set; }
        public string Genero { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new CreateFilmeValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
