using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
