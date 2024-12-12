using MediatR;

namespace Account.Application.UseCases.BankAccount.Commands.Delete;

public record DeleteAccountCommand(int Id) : IRequest;
