using Application.DTO.UserDTOs;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetCurrentUserQuery : IRequest<LoggedInUserDTO> { }
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, LoggedInUserDTO>
    {
        private readonly IUserRepository _userRepository;

        public GetCurrentUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoggedInUserDTO> Handle(GetCurrentUserQuery request, CancellationToken token)
        {
            User user = await _userRepository.GetCurrentUser();
            if (user == null) { return null; }
            LoggedInUserDTO loggedInUser = new LoggedInUserDTO()
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                UserName = user.UserName,
            };
            return loggedInUser;
        }
    }
}
