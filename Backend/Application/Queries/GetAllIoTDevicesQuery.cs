using Application.DTO.IoTDTOs;
using Application.Repositories;
using MediatR;

namespace Application.Queries
{
    public class GetAllIoTDevicesQuery : IRequest<List<IoTDTO>>
    {
        public GetAllIoTDevicesQuery() { }
    }

    public class GetAllIoTDevicesQueryHandler : IRequestHandler<GetAllIoTDevicesQuery, List<IoTDTO>>
    {
        private readonly IIotDeviceRepository _repository;

        public GetAllIoTDevicesQueryHandler(IIotDeviceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<IoTDTO>> Handle(GetAllIoTDevicesQuery request, CancellationToken token)
        {
            return await _repository.GetIoTDevices();
        }
    }
}