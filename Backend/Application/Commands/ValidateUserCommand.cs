using Application.DTO;
using Application.Repositories;
using MediatR;

namespace Application.Commands
{
    public class ValidateUserCommand : IRequest<bool>
    {
        public readonly LoginUserDTO _loginDTO;
        public ValidateUserCommand(LoginUserDTO loginDTO)
        {
            _loginDTO = loginDTO;
        }
    }

    public class ValidateUserCommandHandler : IRequestHandler<ValidateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public ValidateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ValidateUserCommand command, CancellationToken token)
        {
            return await _userRepository.ValidateUserAsync(command._loginDTO);
        }
    }
}
