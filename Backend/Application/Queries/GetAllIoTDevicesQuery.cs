using Application.DTO.IoTDTOs;
using Application.Repositories;
using MediatR;

namespace Application.Queries
{
    public class GetAllIoTDevicesQuery : IRequest<List<CreateIoTDTO>>
    {
        public GetAllIoTDevicesQuery() { }
    }

    public class GetAllIoTDevicesQueryHandler : IRequestHandler<GetAllIoTDevicesQuery, List<CreateIoTDTO>>
    {
        private readonly IIotDeviceRepository _repository;

        public GetAllIoTDevicesQueryHandler(IIotDeviceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CreateIoTDTO>> Handle(GetAllIoTDevicesQuery request, CancellationToken token)
        {
            return await _repository.GetIoTDevices();
        }
    }
}