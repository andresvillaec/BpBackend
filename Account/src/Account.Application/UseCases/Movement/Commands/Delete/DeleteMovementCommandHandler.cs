using Account.Application.Data;
using Account.Domain.Exceptions;
using Account.Domain.Interfaces;
using MediatR;

namespace Account.Application.UseCases.Movement.Commands.Delete;

public sealed class DeleteMovementCommandHandler : IRequestHandler<DeleteMovementCommand>
{
    private readonly IMovementRepository _movementRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMovementCommandHandler(IMovementRepository movementRepository, IUnitOfWork unitOfWork)
    {
        _movementRepository = movementRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteMovementCommand command, CancellationToken cancellationToken)
    {
        var account = await _movementRepository.GetByIdAsync(command.Id);
        if (account == null)
        {
            throw new MovementNotFoundException(command.Id);
        }
        await _movementRepository.DeleteAsync(command.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
