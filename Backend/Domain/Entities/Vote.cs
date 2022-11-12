using System;

namespace Domain.Entities
{
    public class Vote
    {
        public Guid Id { get; set; }
        public bool Positive { get; set; }
        public virtual User User { get; set; }
        public virtual Poll Poll { get; set; }
    }
}
