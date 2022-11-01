using Application.Repositories;
using MediatR;

namespace Application.Commands
{
    public class DeletePollCommand : IRequest<int>
    {
        public readonly Guid _pollId;

        public DeletePollCommand(Guid pollId)
        {
            _pollId = pollId;
        }
    }
    public class DeletePollCommandHandler : IRequestHandler<DeletePollCommand, int>
    {
        private readonly IPollRepository _pollRepository;

        public DeletePollCommandHandler(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public async Task<int> Handle(DeletePollCommand command, CancellationToken token)
        {
            return await _pollRepository.DeletePollAsync(command._pollId);
        }
    }
}