using Account.Application.Data;
using Account.Domain.Exceptions;
using Account.Domain.Interfaces;
using MediatR;

namespace Account.Application.UseCases.Movement.Commands.Update;

public sealed class UpdateMovementCommandHandler : IRequestHandler<UpdateMovementCommand>
{
    private readonly IMovementRepository _movementRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMovementCommandHandler(IMovementRepository movementRepository,
        IUnitOfWork unitOfWork,
        IAccountRepository accountRepository)
    {
        _movementRepository = movementRepository;
        _unitOfWork = unitOfWork;
        _accountRepository = accountRepository;
    }

    public async Task Handle(UpdateMovementCommand command, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByAccountNumberAsync(command.AccountNumber);
        if (account == null)
        {
            throw new AccountNotFoundException(command.Id);
        }

        var movement = await _movementRepository.GetByIdAsync(command.Id);
        if (movement == null)
        {
            throw new MovementNotFoundException(command.Id);
        }

        decimal newBalance = movement.Balance - movement.Amount + command.Amount;
        movement.Update(
            command.Amount,
            newBalance
            );

        await _movementRepository.UpdateAsync(movement);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
