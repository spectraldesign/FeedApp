using Application.DTO;
using Application.Extentions;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetUserQuery : IRequest<UserDTO>
    {
        public string id { get; set; }

    }
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDTO>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken token)
        {
            User user = await _userRepository.GetUserById(request.id);
            return user.ToUserDTO();
        }
    }
}
