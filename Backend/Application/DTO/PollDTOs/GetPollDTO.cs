﻿namespace Application.DTO.PollDTOs
{
    public class GetPollDTO
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsClosed { get; set; }
        public DateTime EndTime { get; set; }
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }
        public int CountVotes { get; set; }
        public int PositiveVotes { get; set; }
        public int NegativeVotes { get; set; }
    }
}
