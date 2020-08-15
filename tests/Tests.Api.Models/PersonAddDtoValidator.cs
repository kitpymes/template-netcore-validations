using FluentValidation;
using Kitpymes.Core.Validations.FluentValidation;

namespace Tests.Api.Models
{
    public class PersonAddDtoValidator : AbstractValidator<PersonAddDto>
    {
        public PersonAddDtoValidator()
        {
            RuleFor(_ => _.Age).IsRange(17, 51, "Edad");
            RuleFor(_ => _.Name).IsName("Nombre").IsLess(100);
            RuleFor(_ => _.Email).IsEmailWithMessage("El correo eléctronico tiene un formato incorrecto.");
        }
    }
}
