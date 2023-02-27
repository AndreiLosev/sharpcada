using sharpcada.Data.Entities;

namespace sharpcada.Data.Repositories;

public class SettingsRepository : BaseRepository<Setting> , Contracts.IRepository
{
    public SettingsRepository(ApplicationDbContext dbContext): base(dbContext) {}
}

