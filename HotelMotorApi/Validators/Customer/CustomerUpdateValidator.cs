using FluentValidation;
using HotelMotorShared.Dtos.CustomerDTOs;

namespace HotelMotorApi.Validators.Customer
{
    public class CustomerUpdateValidator : AbstractValidator<CustomerUpdateDto>
    {
        public CustomerUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");

            RuleFor(x => x.Email).NotEmpty().WithMessage("El email es obligatorio")
                .EmailAddress().WithMessage("Debe ser un email válido");

            RuleFor(x => x.Phone).NotEmpty().WithMessage("El teléfono es obligatorio")
                .Matches(@"^\+?[0-9]{8,15}$").WithMessage("Formato de teléfono inválido");

            RuleFor(x => x.Address).NotEmpty().WithMessage("La dirección es obligatoria");
        }
    }
}
