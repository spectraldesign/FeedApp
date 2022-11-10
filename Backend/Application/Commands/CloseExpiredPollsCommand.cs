using Application.Repositories;
using MediatR;

namespace Application.Commands
{
    public class CloseExpiredPollsCommand : IRequest<int>
    {
        public CloseExpiredPollsCommand() { }
    }
    public class CloseExpiredPollsCommandHandler : IRequestHandler<CloseExpiredPollsCommand, int>
    {
        private readonly IPollRepository _pollRepository;

        public CloseExpiredPollsCommandHandler(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public async Task<int> Handle(CloseExpiredPollsCommand command, CancellationToken token)
        {
            return await _pollRepository.CloseExpiredPolls();
        }
    }
}
