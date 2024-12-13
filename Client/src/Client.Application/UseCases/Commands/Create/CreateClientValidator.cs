using FluentValidation;

namespace Client.Application.UseCases.Commands.Create;

public sealed class CreateClientValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Gender)
            .NotEmpty()
            .IsInEnum();

        RuleFor(x => x.Age)
            .GreaterThan(0)
            .LessThanOrEqualTo(160);

        RuleFor(x => x.DocumentNumber)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(25);

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
    }
}
