using System.ComponentModel.DataAnnotations;

namespace Application.DTO.PollDTOs
{
    public class CreatePollDTO
    {
        [Required]
        public string Question { get; set; }
        [Required]
        public bool IsPrivate { get; set; }
        public DateTime EndTime { get; set; }
    }
}
