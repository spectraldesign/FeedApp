namespace Domain.Entities
{
    public class PollResult
    {
        public string Id { get; set; }

        public string Question  { get; set; }
        public int PositiveVotes { get; set; }
        public int NegativeVotes { get; set; }

        public int TotalVotes { get; set; }
    }
}