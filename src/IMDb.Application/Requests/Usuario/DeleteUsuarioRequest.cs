using IMDb.Application.Validators.Usuario;
using IMDb.Core;

namespace IMDb.Application.Requests.Usuario
{
    public class DeleteUsuarioRequest : Command
    {
        public long Id { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new DeleteUsuarioValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
