using sharpcada.Data.Entities;


namespace sharpcada.Data.Repositories;

public class DeviceRepositories : BaseRepository<Device> , Contracts.IBaseRepository
{
    public DeviceRepositories(ApplicationDbContext dbContext) : base(dbContext) {}
}

