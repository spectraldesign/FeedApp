using Application.DTO.IoTDTOs;
using Application.Repositories;
using MediatR;

namespace Application.Queries
{
    public class GetIoTDeviceQuery : IRequest<IoTDTO>
    {
        public Guid _deviceId { get; set; }
        public GetIoTDeviceQuery(Guid id)
        {
            _deviceId = id;
        }
    }

    public class GetIoTDeviceQueryHandler : IRequestHandler<GetIoTDeviceQuery, IoTDTO>
    {
        private readonly IIotDeviceRepository _repository;

        public GetIoTDeviceQueryHandler(IIotDeviceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IoTDTO> Handle(GetIoTDeviceQuery request, CancellationToken token)
        {
            return await _repository.GetIoTDeviceById(request._deviceId);
        }
    }
}