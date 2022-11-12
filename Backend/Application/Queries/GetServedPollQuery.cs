using Application.DTO.PollDTOs;
using Application.Repositories;
using MediatR;

namespace Application.Queries
{
    public class GetServedPollQuery : IRequest<List<GetPollDTO>>
    {
        public readonly Guid _id;

        public GetServedPollQuery(Guid id)
        {
            _id = id;
        }
    }
    public class GetServedPollsCommandHandler : IRequestHandler<GetServedPollQuery, List<GetPollDTO>>
    {
        private readonly IIotDeviceRepository _iotRepository;

        public GetServedPollsCommandHandler(IIotDeviceRepository iotRepository)
        {
            _iotRepository = iotRepository;
        }

        public async Task<List<GetPollDTO>> Handle(GetServedPollQuery command, CancellationToken token)
        {
            return await _iotRepository.GetServedPolls(command._id);
        }
    }
}