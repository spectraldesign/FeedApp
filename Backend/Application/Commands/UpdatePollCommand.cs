using Application.DTO;
using Application.Repositories;
using MediatR;

namespace Application.Commands
{
    public class UpdatePollCommand : IRequest<int>
    {
        public readonly CreatePollDTO _createPollDTO;
        public readonly Guid _pollId;

        public UpdatePollCommand(Guid Id, CreatePollDTO createPollDTO)
        {
            _pollId = Id;
            _createPollDTO = createPollDTO;
        }
    }
    public class UpdatePollCommandHandler : IRequestHandler<UpdatePollCommand, int>
    {
        private readonly IPollRepository _pollRepository;

        public UpdatePollCommandHandler(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public async Task<int> Handle(UpdatePollCommand command, CancellationToken token)
        {
            return await _pollRepository.UpdatePollAsync(command._pollId, command._createPollDTO);
        }
    }
}