namespace Application.DTO
{
    public class CreatePollDTO
    {
        public string Question { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime EndTime { get; set; }
    }
}
