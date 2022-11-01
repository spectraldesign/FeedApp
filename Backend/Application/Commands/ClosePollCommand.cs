using Application.Repositories;
using MediatR;

namespace Application.Commands
{
    public class ClosePollCommand : IRequest<int>
    {
        public readonly Guid _pollId;
        public ClosePollCommand(Guid pollId)
        {
            _pollId = pollId;
        }
    }
    public class ClosePollCommandHandler : IRequestHandler<ClosePollCommand, int>
    {
        private readonly IPollRepository _pollRepository;

        public ClosePollCommandHandler(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public async Task<int> Handle(ClosePollCommand command, CancellationToken token)
        {
            return await _pollRepository.ClosePollAsync(command._pollId);
        }
    }
}
