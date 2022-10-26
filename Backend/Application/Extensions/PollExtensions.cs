using Application.DTO;
using Domain.Entities;

namespace Application.Extentions
{
    public static class PollExtensions
    {
        public static PollDTO ToPollDTO(this Poll poll)
        {
            return new PollDTO(poll.Id, poll.Question, poll.IsPrivate, poll.IsClosed, poll.EndTime, poll.Creator, poll.Votes.Count);
        }

        public static Poll ToPoll(this CreatePollDTO createPollDTO)
        {
            return new Poll
            {
                Question = createPollDTO.Question,
                IsPrivate = createPollDTO.IsPrivate,
                EndTime = createPollDTO.EndTime,
            };

        }
    }
}
