using sharpcada.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace sharpcada.Data.Repositories;

public class NetworkChannelRepository : BaseRepository<Device> , Contracts.IRepository
{
    public NetworkChannelRepository(ApplicationDbContext dbContext) : base(dbContext) {}

    public async Task LoadFor(Device device) =>
        await _dbContext.NetworkChannels
            .Where(n => n.DeviceId == device.Id)
            .LoadAsync();

    public async Task LoadFor(ICollection<Device> devices) =>
        await _dbContext.NetworkChannels
            .Where(n => devices.Select(d => d.Id).Contains(n.DeviceId))
            .LoadAsync();
}

