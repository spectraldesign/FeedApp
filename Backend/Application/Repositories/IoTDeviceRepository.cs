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
        Task<IoTDTO> GetIoTDeviceById(Guid Id);
        Task<List<IoTDTO>> GetIoTDevices();
        Task<int> RegisterIoTDevice(Guid id);
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

        public async Task<IoTDTO> GetIoTDeviceById(Guid id)
        {
            var response = await _context.IoTDevices.Where(x => x.id == id).Select(p => new IoTDTO() { deviceID = p.id }).FirstOrDefaultAsync();
            return response;
        }

        public async Task<List<IoTDTO>> GetIoTDevices()
        {
            var response = await _context.IoTDevices.Select(p => new IoTDTO() { deviceID = p.id }).ToListAsync();
            return response;
        }

        public async Task<int> RegisterIoTDevice(Guid id)
        {
            var exists = await _context.IoTDevices.Where(x => x.id == id).FirstOrDefaultAsync();
            if (exists != null) { return -1; }
            var response = await _context.IoTDevices.AddAsync(new IoTDevice() { id = id });
            return await _context.SaveChangesAsync();

        }
    }
}
