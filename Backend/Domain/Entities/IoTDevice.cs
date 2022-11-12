using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class IoTDevice
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Poll> PollQueue { get; set; }
    }
}
