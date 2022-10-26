using Application.Repositories;
using MediatR;

namespace Application.Commands
{
    public class DeleteVoteCommand : IRequest<int>
    {
        public readonly Guid _voteId;
        public DeleteVoteCommand(Guid voteId)
        {
            _voteId = voteId;
        }
    }
    public class DeleteVoteCommandHandler : IRequestHandler<DeleteVoteCommand, int>
    {
        private readonly IVoteRepository _voteRepository;
        public DeleteVoteCommandHandler(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public async Task<int> Handle(DeleteVoteCommand command, CancellationToken token)
        {
            return await _voteRepository.DeleteVote(command._voteId);
        }
    }
}
