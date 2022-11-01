using Domain.Entities;

namespace Application.DTO.VoteDTOs
{
    public class VoteDTO
    {
        public Guid Id { get; set; }
        public bool IsPositive { get; set; }
        public User Voter { get; set; }
        public Poll VotedPoll { get; set; }

        public VoteDTO(Guid id, bool isPositive, User voter, Poll votedPoll)
        {
            Id = id;
            IsPositive = isPositive;
            Voter = voter;
            VotedPoll = votedPoll;
        }
    }
}