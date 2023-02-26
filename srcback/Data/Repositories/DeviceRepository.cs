using sharpcada.Data.Entities;


namespace sharpcada.Data.Repositories;

public class DeviceRepository : BaseRepository<Device> , Contracts.IRepository
{
    public DeviceRepository(ApplicationDbContext dbContext) : base(dbContext) {}
}

