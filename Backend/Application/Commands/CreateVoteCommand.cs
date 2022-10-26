using Application.DTO;
using Application.Repositories;
using MediatR;

namespace Application.Commands
{
    public class CreateVoteCommand : IRequest<int>
    {
        public readonly CreateVoteDTO _createVoteDTO;
        public readonly Guid _pollId;

        public CreateVoteCommand(Guid pollId, CreateVoteDTO createVoteDTO)
        {
            _createVoteDTO = createVoteDTO;
            _pollId = pollId;
        }
    }
    public class CreateVoteCommandHandler : IRequestHandler<CreateVoteCommand, int>
    {
        private readonly IVoteRepository _voteRepository;

        public CreateVoteCommandHandler(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public async Task<int> Handle(CreateVoteCommand command, CancellationToken token)
        {
            return await _voteRepository.CreateVote(command._pollId, command._createVoteDTO);
        }
    }
}
