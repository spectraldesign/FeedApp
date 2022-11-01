using System.ComponentModel.DataAnnotations;

namespace Application.DTO.VoteDTOs
{
    public class CreateVoteDTO
    {
        [Required]
        public bool IsPositive { get; set; }
    }
}
