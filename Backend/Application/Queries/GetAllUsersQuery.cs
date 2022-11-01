using Application.DTO.UserDTOs;
using Application.Extentions;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetAllUsersQuery : IRequest<List<UserDTO>> { }
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDTO>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken token)
        {
            List<User> users = await _userRepository.GetAllUsers();
            return users.Select(x => x.ToUserDTO()).ToList();
        }
    }
}
