using Account.Application.Data;
using Account.Domain.Exceptions;
using Account.Domain.Interfaces;
using MediatR;

namespace Account.Application.UseCases.BankAccount.Commands.Create;

public sealed class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        bool isDuplicatedAccountNumber = await _accountRepository.ExistsAccountNumber(command.Number);
        if (isDuplicatedAccountNumber)
        {
            throw new DuplicatedAccountException(command.Number);
        }

        Domain.Entities.Account account = new(
            command.Number,
            command.AccountType,
            command.OpeningDeposit,
            command.Balance,
            command.Status,
            command.ClientId
            );

        await _accountRepository.AddAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
