using Domain.Entities;

namespace Application.DTO.PollDTOs
{
    public class PollDTO
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsClosed { get; set; }
        public DateTime EndTime { get; set; }
        public User Creator { get; set; }
        public int CountVotes { get; set; }

        public PollDTO(string id, string question, bool isPrivate, bool isClosed, DateTime endTime, User creator, int countVotes)
        {
            Id = id;
            Question = question;
            IsPrivate = isPrivate;
            IsClosed = isClosed;
            EndTime = endTime;
            Creator = creator;
            CountVotes = countVotes;
        }
    }
}
