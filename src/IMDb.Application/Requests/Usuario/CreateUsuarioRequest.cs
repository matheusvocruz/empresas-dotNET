using IMDb.Application.Validators.Usuario;
using IMDb.Core;

namespace IMDb.Application.Requests.Usuario
{
    public class CreateUsuarioRequest : Command
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Administrador { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new CreateUsuarioValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
