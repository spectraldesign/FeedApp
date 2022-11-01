using Application.DTO.UserDTOs;
using Application.Extentions;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(string id);
        Task<List<User>> GetAllUsers();
        Task<IdentityResult> CreateUser(CreateUserDTO createUserDTO);
        Task<IdentityResult> UpdateUserAsync(UpdateUserDTO updateUserDTO);
        Task<bool> ValidateUserAsync(LoginUserDTO loginDto);
        Task<string> CreateTokenAsync();
    }
    public class UserRepository : IUserRepository
    {
        private readonly IFeedAppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User _user;
        private readonly IGenericExtension _genericExtension;

        public UserRepository(IGenericExtension genericExtension, IFeedAppDbContext context, UserManager<User> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _genericExtension = genericExtension;
        }
        public async Task<User> GetUserById(string id)
        {
            return await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IdentityResult> CreateUser(CreateUserDTO userRegistration)
        {
            var user = userRegistration.ToUser();
            return await _userManager.CreateAsync(user, userRegistration.Password);
        }

        public async Task<IdentityResult> UpdateUserAsync(UpdateUserDTO updateUser)
        {
            User currentUser = await _genericExtension.GetCurrentUserAsync();
            var dbUser = await _context.Users.Where(x => x.Id == currentUser.Id).FirstOrDefaultAsync();
            if (updateUser.Firstname != null) { dbUser.Firstname = updateUser.Firstname; }
            if (updateUser.Lastname != null) { dbUser.Lastname = updateUser.Lastname; }
            if (updateUser.UserName != null) { dbUser.UserName = updateUser.UserName; }
            if (updateUser.Email != null) { dbUser.Email = updateUser.Email; }
            if (updateUser.Password != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(dbUser);
                IdentityResult passwordChangeResult = await _userManager.ResetPasswordAsync(dbUser, resetToken, updateUser.Password);
            }

            IdentityResult updateUserResult = await _userManager.UpdateAsync(dbUser);
            return updateUserResult;
        }

        public async Task<bool> ValidateUserAsync(LoginUserDTO loginDTO)
        {
            _user = await _userManager.FindByNameAsync(loginDTO.UserName);
            var result = _user != null && await _userManager.CheckPasswordAsync(_user, loginDTO.Password);
            return result;
        }

        public async Task<string> CreateTokenAsync()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtConfig = _configuration.GetSection("jwtConfig");
            var key = Encoding.UTF8.GetBytes(jwtConfig["Secret"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, _user.UserName) };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtConfig");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiresIn"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}
