using Microsoft.EntityFrameworkCore;
using sharpcada.Data.Entities;

namespace sharpcada.Data.Repositories;

public abstract class BaseRepository<TEntity> : SimpleBaseRepository<TEntity>
    where TEntity : EntityBase
{
    public BaseRepository(ApplicationDbContext dbContext) : base(dbContext) {}

    public async Task<ICollection<TEntity>> GetAsync(ICollection<long> ids) =>
        await _dbContext.Set<TEntity>().Where(e => ids.Contains(e.Id)).ToListAsync();
}
