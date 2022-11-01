using Application.DTO.VoteDTOs;
using Application.Repositories;
using MediatR;

namespace Application.Queries
{
    public class GetYourVotesQuery : IRequest<List<GetVoteDTO>>
    {
        public GetYourVotesQuery() { }
    }
    public class GetYourVotesQueryHandler : IRequestHandler<GetYourVotesQuery, List<GetVoteDTO>>
    {
        private readonly IVoteRepository _voteRepository;
        public GetYourVotesQueryHandler(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }
        public async Task<List<GetVoteDTO>> Handle(GetYourVotesQuery request, CancellationToken token)
        {
            return await _voteRepository.GetYourVotes();
        }
    }
}
