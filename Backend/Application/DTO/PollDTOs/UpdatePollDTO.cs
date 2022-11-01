namespace Application.DTO.PollDTOs
{
    public class UpdatePollDTO
    {
        public string Question { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime EndTime { get; set; }
    }
}
