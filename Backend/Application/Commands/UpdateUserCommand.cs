using Application.DTO.UserDTOs;
using Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands
{
    public class UpdateUserCommand : IRequest<IdentityResult>
    {
        public readonly UpdateUserDTO _updateUserDTO;
        public readonly string _userId;
        public UpdateUserCommand(UpdateUserDTO updateDTO)
        {
            _updateUserDTO = updateDTO;
        }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, IdentityResult>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> Handle(UpdateUserCommand command, CancellationToken token)
        {
            return await _userRepository.UpdateUserAsync(command._updateUserDTO);
        }
    }
}
