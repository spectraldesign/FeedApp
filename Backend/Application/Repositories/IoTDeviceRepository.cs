using Application.DTO.IoTDTOs;
using Application.DTO.PollDTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public interface IIotDeviceRepository
    {
        Task<GetIoTWithQueueDTO> GetIoTDeviceById(Guid Id);
        Task<List<CreateIoTDTO>> GetIoTDevices();
        Task<int> RegisterIoTDevice(CreateIoTDTO createIoTDTO);
        Task<int> ServePoll(Guid ioTId, string pollId);
        Task<List<GetPollDTO>> GetServedPolls(Guid ioTId);
    }
    public class IoTDeviceRepository : IIotDeviceRepository
    {
        private readonly IFeedAppDbContext _context;

        public IoTDeviceRepository(IFeedAppDbContext context)
        {
            _context = context;
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
            if (dbPoll.IsPrivate) { return -3; }
            if (dbPoll.IsClosed) { return -4; }
            if (dbIoT.PollQueue == null) { dbIoT.PollQueue = new List<Poll>(); }
            dbIoT.PollQueue.Add(dbPoll);
            var update = _context.IoTDevices.Update(dbIoT);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<GetPollDTO>> GetServedPolls(Guid ioTId)
        {
            var polls = await _context.IoTDevices
                .Where(i => i.Id == ioTId)
                .Select(i => i.PollQueue.Select(p => new GetPollDTO()
                {
                    Id = p.Id,
                    Question = p.Question,
                    IsPrivate = p.IsPrivate,
                    IsClosed = p.IsClosed,
                    EndTime = p.EndTime,
                    CreatorId = p.Creator.Id,
                    CreatorName = p.Creator.UserName,
                    CountVotes = p.Votes.Count,
                    PositiveVotes = p.Votes.Where(v => v.Positive == true).Count(),
                    NegativeVotes = p.Votes.Where(v => v.Positive == false).Count()
                })).FirstOrDefaultAsync();
            return (List<GetPollDTO>)polls;
        }
    }
}
