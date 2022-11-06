namespace Application.DTO.IoTDTOs
{
    public class GetIoTWithQueueDTO
    {
        public Guid DeviceID { get; set; }
        public string DeviceName { get; set; }
        public List<string> PollQueue { get; set; }
    }
}
