namespace Application.DTO
{
    public class GetPollDTO
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsClosed { get; set; }
        public DateTime EndTime { get; set; }
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }
        public int CountVotes { get; set; }
    }
}
