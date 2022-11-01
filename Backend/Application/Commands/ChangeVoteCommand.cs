using Application.DTO.VoteDTOs;
using Application.Repositories;
using MediatR;

namespace Application.Commands
{
    public class ChangeVoteCommand : IRequest<int>
    {
        public readonly CreateVoteDTO _updateVoteDTO;
        public readonly Guid _voteId;

        public ChangeVoteCommand(Guid VoteId, CreateVoteDTO updateVoteDTO)
        {
            _updateVoteDTO = updateVoteDTO;
            _voteId = VoteId;
        }
    }

    public class ChangeVoteCommandHandler : IRequestHandler<ChangeVoteCommand, int>
    {
        private readonly IVoteRepository _voteRepository;

        public ChangeVoteCommandHandler(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public async Task<int> Handle(ChangeVoteCommand command, CancellationToken token)
        {
            return await _voteRepository.ChangeVote(command._voteId, command._updateVoteDTO);
        }
    }
}
