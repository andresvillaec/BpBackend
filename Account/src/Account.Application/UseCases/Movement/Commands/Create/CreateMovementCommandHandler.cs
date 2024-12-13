using Account.Application.Data;
using Account.Domain.Exceptions;
using Account.Domain.Interfaces;
using MediatR;

namespace Account.Application.UseCases.Movement.Commands.Create;

public sealed class CreateMovementCommandHandler : IRequestHandler<CreateMovementCommand>
{
    private readonly IMovementRepository _movementRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMovementCommandHandler(IMovementRepository movementRepository, 
        IUnitOfWork unitOfWork, 
        IAccountRepository accountRepository)
    {
        _movementRepository = movementRepository;
        _unitOfWork = unitOfWork;
        _accountRepository = accountRepository;
    }

    public async Task Handle(CreateMovementCommand command, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByAccountNumberAsync(command.AccountNumber);
        decimal currentBalance = account.Balance;
        decimal newBalance = currentBalance + command.Amount;

        if (newBalance < 0)
        {
            throw new NoFundsAvailable();
        }

        Domain.Entities.Movement movement = new(
            command.Amount,
            newBalance,
            account.Id
            );

        account.UpdateBalance(command.Amount);

        await _movementRepository.AddAsync(movement);
        await _accountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
