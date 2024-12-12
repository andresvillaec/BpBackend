using Account.Application.Data;
using Account.Domain.Interfaces;
using MediatR;

namespace Account.Application.UseCases.Movement.Commands.Create;

public sealed class CreateMovementCommandHandler : IRequestHandler<CreateMovementCommand>
{
    private readonly IMovementRepository _movementRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMovementCommandHandler(IMovementRepository movementRepository, IUnitOfWork unitOfWork)
    {
        _movementRepository = movementRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateMovementCommand command, CancellationToken cancellationToken)
    {
        Domain.Entities.Movement movement = new(
            command.Timestamp,
            command.AccountType,
            command.Amount,
            command.Balance,
            command.AccountNumber
            );

        await _movementRepository.AddAsync(movement);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
