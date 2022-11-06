using Application.DTO.IoTDTOs;
using Application.Extentions;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Repositories
{
    public interface IIotDeviceRepository
    {
        Task<GetIoTWithQueueDTO> GetIoTDeviceById(Guid Id);
        Task<List<CreateIoTDTO>> GetIoTDevices();
        Task<int> RegisterIoTDevice(CreateIoTDTO createIoTDTO);
        Task<int> ServePoll(Guid ioTId, string pollId);
    }
    public class IoTDeviceRepository : IIotDeviceRepository
    {
        private readonly IGenericExtension _genericExtension;
        private readonly IFeedAppDbContext _context;
        private readonly IConfiguration _configuration;

        public IoTDeviceRepository(IGenericExtension genericExtension, IFeedAppDbContext context, IConfiguration configuration)
        {
            _genericExtension = genericExtension;
            _context = context;
            _configuration = configuration;
        }

        public async Task<GetIoTWithQueueDTO> GetIoTDeviceById(Guid id)
        {
            var response = await _context.IoTDevices
                .Where(x => x.Id == id)
                .Select(p => new GetIoTWithQueueDTO()
                {
                    DeviceID = p.Id,
                    DeviceName = p.Name,
                    PollQueue = p.PollQueue.Select(q => q.Id).ToList()
                }).FirstOrDefaultAsync();
            return response;
        }

        public async Task<List<CreateIoTDTO>> GetIoTDevices()
        {
            var response = await _context.IoTDevices.Select(p => new CreateIoTDTO() { deviceID = p.Id, deviceName = p.Name }).ToListAsync();
            return response;
        }

        public async Task<int> RegisterIoTDevice(CreateIoTDTO createIoTDTO)
        {
            var exists = await _context.IoTDevices.Where(x => x.Id == createIoTDTO.deviceID).FirstOrDefaultAsync();
            if (exists != null) { return -1; }
            var response = await _context.IoTDevices.AddAsync(new IoTDevice() { Id = createIoTDTO.deviceID, Name = createIoTDTO.deviceName, PollQueue = new List<Poll>() });
            return await _context.SaveChangesAsync();

        }

        public async Task<int> ServePoll(Guid ioTId, string pollId)
        {
            var dbIoT = await _context.IoTDevices.Where(x => x.Id == ioTId).FirstOrDefaultAsync();
            if (dbIoT == null) { return -1; }
            var dbPoll = await _context.Polls.Where(x => x.Id == pollId).FirstOrDefaultAsync();
            if (dbPoll == null) { return -2; }
            if (dbIoT.PollQueue == null) { dbIoT.PollQueue = new List<Poll>(); }
            dbIoT.PollQueue.Add(dbPoll);
            var update = _context.IoTDevices.Update(dbIoT);
            return await _context.SaveChangesAsync();
        }
    }
}
