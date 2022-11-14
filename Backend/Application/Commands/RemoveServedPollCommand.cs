using Application.Repositories;
using MediatR;

namespace Application.Commands
{
    public class RemoveServedPollCommand : IRequest<int>
    {
        public readonly Guid _ioTId;
        public readonly string _pollId;

        public RemoveServedPollCommand(Guid IoTId, string pollId)
        {
            _ioTId = IoTId;
            _pollId = pollId;
        }
    }
    public class RemoveServedPollCommandHandler : IRequestHandler<RemoveServedPollCommand, int>
    {
        private readonly IIotDeviceRepository _iotRepository;

        public RemoveServedPollCommandHandler(IIotDeviceRepository iotRepository)
        {
            _iotRepository = iotRepository;
        }

        public async Task<int> Handle(RemoveServedPollCommand command, CancellationToken token)
        {
            return await _iotRepository.RemoveServedPoll(command._ioTId, command._pollId);
        }
    }
}