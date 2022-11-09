using Application.DTO.PollDTOs;
using Application.Repositories;
using MediatR;

namespace Application.Queries
{
    public class GetUserPollsQuery : IRequest<List<GetPollIdDTO>> { }

    public class GetUserPollsQueryHandler : IRequestHandler<GetUserPollsQuery, List<GetPollIdDTO>>
    {
        private readonly IPollRepository _pollRepository;

        public GetUserPollsQueryHandler(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public async Task<List<GetPollIdDTO>> Handle(GetUserPollsQuery request, CancellationToken token)
        {
            return await _pollRepository.GetUserPolls();
        }
    }
}