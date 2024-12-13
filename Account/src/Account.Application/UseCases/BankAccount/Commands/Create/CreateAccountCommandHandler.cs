using Account.Application.Data;
using Account.Application.UseCases.BankAccount.Commands.Update;
using Account.Application.UseCases.BankAccount.Queries.Get;
using Account.Application.UseCases.Movement.Queries.Get;
using Account.Domain.Exceptions;
using Account.Domain.Interfaces;
using MediatR;

namespace Account.Application.UseCases.BankAccount.Commands.Create;

public sealed class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountResponse>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AccountResponse> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
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
            command.ClientId
            );

        await _accountRepository.AddAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AccountResponse(
                account.Id,
                account.Number,
                account.AccountType,
                account.OpeningDeposit,
                account.Balance,
                account.Status,
                account.ClientId);
    }
}
