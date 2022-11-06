using System.ComponentModel.DataAnnotations;

namespace Application.DTO.IoTDTOs
{
    public class CreateIoTDTO
    {
        [Required]
        public Guid deviceID { get; set; }
        public string deviceName { get; set; }
    }
}
