using Application.DTO.PollDTOs;
using Application.Repositories;
using MediatR;

namespace Application.Commands
{
    public class UpdatePollCommand : IRequest<int>
    {
        public readonly UpdatePollDTO _updatePollDTO;
        public readonly string _pollId;

        public UpdatePollCommand(string Id, UpdatePollDTO updatePollDTO)
        {
            _pollId = Id;
            _updatePollDTO = updatePollDTO;
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
            return await _pollRepository.UpdatePollAsync(command._pollId, command._updatePollDTO);
        }
    }
}