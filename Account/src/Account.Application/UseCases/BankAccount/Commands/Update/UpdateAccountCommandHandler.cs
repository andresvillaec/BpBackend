using Account.Application.Data;
using Account.Domain.Exceptions;
using Account.Domain.Interfaces;
using MediatR;

namespace Account.Application.UseCases.BankAccount.Commands.Update;

public sealed class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateAccountCommand command, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(command.Id);
        if (account == null)
        {
            throw new AccountNotFoundException(command.Id);
        }

        account.Update(
            command.Number,
            command.OpeningDeposit,
            command.Balance,
            command.Status,
            command.ClientId
            );

        await _accountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
