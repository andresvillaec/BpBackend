using Client.Application.Client.Commands.Create;
using FluentValidation;

namespace Client.Application.Validator
{
    public sealed class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");
        }
    }
}
