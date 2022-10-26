using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Domain.Interfaces
{
    public interface IFeedAppDbContext
    {
        public DbSet<Poll> Polls { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
