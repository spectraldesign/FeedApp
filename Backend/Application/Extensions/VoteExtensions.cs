using Application.DTO;
using Domain.Entities;

namespace Application.Extensions
{
    public static class VoteExtensions
    {
        public static VoteDTO ToVoteDTO(this Vote vote)
        {
            return new VoteDTO(vote.Id, vote.Positive, vote.User, vote.Poll);
        }

        public static Vote ToVote(this CreateVoteDTO createVoteDTO)
        {
            return new Vote() { Positive = createVoteDTO.IsPositive };
        }
    }
}
