using Application.DTO.IoTDTOs;
using Application.Repositories;
using MediatR;

namespace Application.Commands
{
    public class RegisterIoTDeviceCommand : IRequest<int>
    {
        public readonly IoTDTO _ioTDTO;

        public RegisterIoTDeviceCommand(IoTDTO ioTDTO)
        {
            _ioTDTO = ioTDTO;
        }
    }

    public class RegisterIoTDeviceCommandHandler : IRequestHandler<RegisterIoTDeviceCommand, int>
    {
        private readonly IIotDeviceRepository _repository;

        public RegisterIoTDeviceCommandHandler(IIotDeviceRepository ioTRepository)
        {
            _repository = ioTRepository;
        }

        public async Task<int> Handle(RegisterIoTDeviceCommand command, CancellationToken token)
        {
            return await _repository.RegisterIoTDevice(command._ioTDTO.deviceID);
        }
    }
}