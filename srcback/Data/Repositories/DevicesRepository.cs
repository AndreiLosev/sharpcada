using sharpcada.Data.Entities;


namespace sharpcada.Data.Repositories;

public class DevicesRepository : BaseRepository<Device> , Contracts.IRepository
{
    public DevicesRepository(ApplicationDbContext dbContext) : base(dbContext) {}
}

