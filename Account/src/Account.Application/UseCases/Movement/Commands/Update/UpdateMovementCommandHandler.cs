﻿using Account.Application.Data;
using Account.Domain.Exceptions;
using Account.Domain.Interfaces;
using MediatR;

namespace Account.Application.UseCases.Movement.Commands.Update;

public sealed class UpdateMovementCommandHandler : IRequestHandler<UpdateMovementCommand>
{
    private readonly IMovementRepository _movementRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMovementCommandHandler(IMovementRepository movementRepository, IUnitOfWork unitOfWork)
    {
        _movementRepository = movementRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateMovementCommand command, CancellationToken cancellationToken)
    {
        //TODO: Implement it
        decimal newBalance = 0;
        var movement = await _movementRepository.GetByIdAsync(command.Id);
        if (movement == null)
        {
            throw new MovementNotFoundException(command.Id);
        }

        movement.Update(
            command.Amount,
            newBalance
            );

        await _movementRepository.UpdateAsync(movement);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}