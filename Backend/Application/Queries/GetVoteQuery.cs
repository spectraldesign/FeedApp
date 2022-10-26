using Application.DTO;
using Application.Repositories;
using MediatR;

namespace Application.Queries
{
    public class GetVoteQuery : IRequest<GetVoteDTO>
    {
        public Guid _voteId { get; set; }
        public GetVoteQuery(Guid voteId)
        {
            _voteId = voteId;
        }
    }
    public class GetVoteQueryHandler : IRequestHandler<GetVoteQuery, GetVoteDTO>
    {
        private readonly IVoteRepository _voteRepository;

        public GetVoteQueryHandler(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public async Task<GetVoteDTO> Handle(GetVoteQuery request, CancellationToken token)
        {
            return await _voteRepository.GetVoteById(request._voteId);
        }
    }
}
