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
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var account = await _accountRepository.GetByAccountNumberAsync(command.AccountNumber) ?? throw new AccountNotFoundException(command.Id);
            var movement = await _movementRepository.GetByIdAsync(command.Id) ?? throw new MovementNotFoundException(command.Id);

            decimal newMovementAmount = command.Amount - movement.Amount;
            decimal newBalance = movement.Balance + newMovementAmount;
            movement.Update(command.Amount, newBalance);

            account.UpdateBalance(newMovementAmount);

            await _movementRepository.UpdateAsync(movement);
            await _accountRepository.UpdateAsync(account);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
