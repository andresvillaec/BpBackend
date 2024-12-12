using Account.Application.Data;
using Account.Domain.Exceptions;
using Account.Domain.Interfaces;
using MediatR;

namespace Account.Application.UseCases.BankAccount.Commands.Delete
{
    public sealed class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteAccountCommand command, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(command.Id);
            if (account == null)
            {
                throw new AccountNotFoundException(command.Id);
            }
            await _accountRepository.DeleteAsync(command.Id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
