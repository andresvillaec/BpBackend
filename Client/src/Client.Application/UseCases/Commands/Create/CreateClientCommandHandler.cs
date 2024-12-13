using Client.Application.Data;
using Client.Application.UseCases.Queries.Get;
using Client.Domain.Interfaces;
using MediatR;

namespace Client.Application.UseCases.Commands.Create;

public sealed class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ClientResponse>
{
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClientCommandHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ClientResponse> Handle(CreateClientCommand command, CancellationToken cancellationToken)
    {
        Domain.Entities.Client client = new(
            command.Name,
            command.Gender,
            command.Age,
            command.DocumentNumber,
            command.Address,
            command.Phone,
            command.Password
            );

        await _clientRepository.AddAsync(client);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ClientResponse(
                client.Id,
                client.Name,
                client.Gender,
                client.Age,
                client.DocumentNumber,
                client.Address,
                client.Phone,
                client.Status);
    }
}
