using Client.Application.UseCases.Commands.Create;
using FluentValidation;

namespace Client.Application.Validator
{
    public sealed class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("El nombre no puede ser mayor a 100 caracteres");

            RuleFor(x => x.Gender)
                .NotEmpty()
                .IsInEnum()
                .WithMessage("El género no existe");

            RuleFor(x => x.Age)
                .GreaterThan(0)
                .LessThanOrEqualTo(160)
                .WithMessage("Rango de edad invalido");

            RuleFor(x => x.DocumentNumber)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(25)
                .Matches("^[a-zA-Z0-9]*$").WithMessage("Formato incorrecto de número de documento");

            RuleFor(x => x.Address)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(25);

            RuleFor(x => x.Phone)
               .NotEmpty()
               .MinimumLength(2)
               .MaximumLength(20);

            RuleFor(x => x.Password)
               .NotEmpty()
               .MinimumLength(5)
               .MaximumLength(25);

            RuleFor(x => x.Status)
               .NotEmpty();
        }
    }
}
