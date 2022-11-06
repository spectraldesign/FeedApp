using Application.DTO.IoTDTOs;
using Application.Repositories;
using MediatR;

namespace Application.Queries
{
    public class GetIoTDeviceQuery : IRequest<GetIoTWithQueueDTO>
    {
        public Guid _deviceId { get; set; }
        public GetIoTDeviceQuery(Guid id)
        {
            _deviceId = id;
        }
    }

    public class GetIoTDeviceQueryHandler : IRequestHandler<GetIoTDeviceQuery, GetIoTWithQueueDTO>
    {
        private readonly IIotDeviceRepository _repository;

        public GetIoTDeviceQueryHandler(IIotDeviceRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetIoTWithQueueDTO> Handle(GetIoTDeviceQuery request, CancellationToken token)
        {
            return await _repository.GetIoTDeviceById(request._deviceId);
        }
    }
}