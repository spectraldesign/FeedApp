using Application.Repositories;
using MediatR;

namespace Application.Commands
{
    public class ServePollCommand : IRequest<int>
    {
        public readonly Guid _id;
        public readonly string _pollId;

        public ServePollCommand(Guid id, string pollId)
        {
            _pollId = pollId;
            _id = id;
        }
    }
    public class ServePollCommandHandler : IRequestHandler<ServePollCommand, int>
    {
        private readonly IIotDeviceRepository _iotRepository;

        public ServePollCommandHandler(IIotDeviceRepository iotRepository)
        {
            _iotRepository = iotRepository;
        }

        public async Task<int> Handle(ServePollCommand command, CancellationToken token)
        {
            return await _iotRepository.ServePoll(command._id, command._pollId);
        }
    }
}