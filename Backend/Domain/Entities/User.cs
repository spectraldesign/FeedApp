
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public virtual ICollection<Vote> Votes { get; init; }
        public virtual ICollection<Poll> CreatedPolls { get; init; }
        public bool IsAdmin { get; set; }
    }
}
