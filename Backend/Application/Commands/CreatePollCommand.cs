using Application.DTO.PollDTOs;
using Application.Repositories;
using MediatR;
namespace Application.Commands
{
    public class CreatePollCommand : IRequest<int>
    {
        public readonly CreatePollDTO _createPollDTO;

        public CreatePollCommand(CreatePollDTO createPollDTO)
        {
            _createPollDTO = createPollDTO;
            Console.WriteLine(_createPollDTO);
        }
    }

    public class CreatePollCommandHandler : IRequestHandler<CreatePollCommand, int>
    {
        private readonly IPollRepository _pollRepository;

        public CreatePollCommandHandler(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public async Task<int> Handle(CreatePollCommand command, CancellationToken token)
        {
            return await _pollRepository.CreatePoll(command._createPollDTO);
        }
    }
}