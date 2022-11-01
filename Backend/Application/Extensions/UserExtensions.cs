using Application.DTO.UserDTOs;
using Domain.Entities;
namespace Application.Extentions
{
    public static class UserExtensions
    {
        public static UserDTO ToUserDTO(this User user)
        {
            return new UserDTO { Id = user.Id, UserName = user.UserName };
        }

        public static User ToUser(this CreateUserDTO createUserDTO)
        {
            return new User
            {
                UserName = createUserDTO.UserName,
                Email = createUserDTO.Email,
                Firstname = createUserDTO.Firstname,
                Lastname = createUserDTO.Lastname,
            };
        }
    }
}
