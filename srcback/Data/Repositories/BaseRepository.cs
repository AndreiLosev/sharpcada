using Microsoft.EntityFrameworkCore;

namespace sharpcada.Data.Repositories;

public abstract class BaseRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _dbContext;

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(long id) =>
        await _dbContext.FindAsync<TEntity>(id);

    public async Task<List<TEntity>> AllAsync() =>
        await _dbContext.Set<TEntity>().ToListAsync();
}
