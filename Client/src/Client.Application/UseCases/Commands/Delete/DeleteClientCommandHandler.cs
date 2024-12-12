using Client.Application.Data;
using Client.Domain.Exceptions;
using Client.Domain.Interfaces;
using MediatR;

namespace Client.Application.UseCases.Commands.Delete;

public sealed class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
{
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClientCommandHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteClientCommand command, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByIdAsync(command.Id);
        if (client == null)
        {
            throw new ClientNotFoundException(command.Id);
        }
        await _clientRepository.DeleteAsync(command.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}