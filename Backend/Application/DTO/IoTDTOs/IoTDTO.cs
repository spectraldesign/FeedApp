using System.ComponentModel.DataAnnotations;

namespace Application.DTO.IoTDTOs
{
    public class IoTDTO
    {
        [Required]
        public Guid deviceID { get; set; }
    }
}
