using Application.DTO;
using Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands
{
    public class CreateUserCommand : IRequest<IdentityResult>
    {
        public readonly CreateUserDTO _createUserDTO;
        public CreateUserCommand(CreateUserDTO createUserDTO)
        {
            _createUserDTO = createUserDTO;
        }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IdentityResult>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> Handle(CreateUserCommand command, CancellationToken token)
        {
            return await _userRepository.CreateUser(command._createUserDTO);
        }
    }
}
