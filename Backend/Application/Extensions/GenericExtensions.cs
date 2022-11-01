using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Extentions
{
    public interface IGenericExtension
    {
        Task<User> GetCurrentUserAsync();
        Task<bool> IsCurrentUserAdminAsync();
    }
    public class GenericExtensions : IGenericExtension
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFeedAppDbContext _context;
        public GenericExtensions(IHttpContextAccessor httpContextAccessor, IFeedAppDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public async Task<User> GetCurrentUserAsync()
        {
            string name = _httpContextAccessor.HttpContext.User.Identity.Name;
            User user = await _context.Users.Where(x => x.UserName == name).FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> IsCurrentUserAdminAsync()
        {
            User user = await GetCurrentUserAsync();
            return user.IsAdmin;
        }
    }
}
