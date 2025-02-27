using FluentValidation;
using System.Data;
using webmotos.Models;

namespace webmotos.Validators
{
    public class TipoRegistrationValidator:AbstractValidator<Tipo>
    {
        public TipoRegistrationValidator()
        {
            RuleFor(x => x.Tipo1)
            .NotEmpty().WithMessage("El tipo es obligatorio")
            .Length(3, 50).WithMessage("Debe tener entre 3 y 50 caracteres");
        }
        
    }
}
