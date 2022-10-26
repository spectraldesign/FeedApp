using Application.DTO;
using Application.Repositories;
using MediatR;

namespace Application.Queries
{
    public class GetAllPollsQuery : IRequest<List<GetPollDTO>> { }

    public class GetAllPollsQueryHandler : IRequestHandler<GetAllPollsQuery, List<GetPollDTO>>
    {
        private readonly IPollRepository _pollRepository;

        public GetAllPollsQueryHandler(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public async Task<List<GetPollDTO>> Handle(GetAllPollsQuery request, CancellationToken token)
        {
            return await _pollRepository.GetAllPolls();
        }
    }
}
