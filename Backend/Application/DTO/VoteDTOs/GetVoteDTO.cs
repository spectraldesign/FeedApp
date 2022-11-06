namespace Application.DTO.VoteDTOs
{
    //Note: Does not return the user that voted to keep votes anonymous.
    public class GetVoteDTO
    {
        public Guid Id { get; set; }
        public bool IsPositive { get; set; }
        public string VotedPollId { get; set; }
        public string VotedPollQuestion { get; set; }
    }
}
