using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Database
{
    public class FeedAppDbContext : IdentityDbContext<User>, IFeedAppDbContext
    {
        public FeedAppDbContext(DbContextOptions<FeedAppDbContext> options) : base(options)
        {

        }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<IoTDevice> IoTDevices { get; set; }
    }
}
