using Application.DTO;
using Application.Repositories;
using MediatR;

namespace Application.Queries
{
    public class GetVotesByPollIdQuery : IRequest<List<GetVoteDTO>>
    {
        public Guid _pollId { get; set; }
        public GetVotesByPollIdQuery(Guid pollId)
        {
            _pollId = pollId;
        }
    }
    public class GetVotesByPollIdQueryHandler : IRequestHandler<GetVotesByPollIdQuery, List<GetVoteDTO>>
    {
        private readonly IVoteRepository _voteRepository;
        public GetVotesByPollIdQueryHandler(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }
        public async Task<List<GetVoteDTO>> Handle(GetVotesByPollIdQuery request, CancellationToken token)
        {
            return await _voteRepository.GetVotesByPollId(request._pollId);
        }
    }
}

