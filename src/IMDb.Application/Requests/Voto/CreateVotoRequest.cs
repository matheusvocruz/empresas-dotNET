using IMDb.Application.Validators.Voto;
using IMDb.Core;

namespace IMDb.Application.Requests.Voto
{
    public class CreateVotoRequest : Command
    {
        public long FilmeId { get; set; }
        public int Valor { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new CreateVotoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
