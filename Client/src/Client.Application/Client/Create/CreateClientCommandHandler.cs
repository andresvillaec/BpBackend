using Client.Application.Data;
using Client.Domain.Interfaces;
using MediatR;

namespace Client.Application.Client.Create
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateClientCommandHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateClientCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Client client = new(
                command.Name,
                command.Gender,
                command.Age,
                command.DocumentNumber,
                command.Address,
                command.Phone,
                command.Password,
                command.Status
                );

            await _clientRepository.AddAsync(client);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
