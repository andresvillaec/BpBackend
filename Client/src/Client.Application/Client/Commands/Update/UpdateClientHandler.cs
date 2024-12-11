using Client.Application.Data;
using Client.Domain.Exceptions;
using Client.Domain.Interfaces;
using MediatR;

namespace Client.Application.Client.Commands.Update
{
    public sealed class UpdateClientHandler : IRequestHandler<UpdateClientCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClientHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateClientCommand command, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(command.Id);
            if (client == null)
            {
                throw new ClientNotFoundException(command.Id);
            }

            client.Update(
               command.Name,
                command.Gender,
                command.Age,
                command.DocumentNumber,
                command.Address,
                command.Phone,
                command.Password,
                command.Status
                );

            await _clientRepository.UpdateAsync(client);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
