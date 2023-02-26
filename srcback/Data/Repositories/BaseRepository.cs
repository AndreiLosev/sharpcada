using Microsoft.EntityFrameworkCore;

namespace sharpcada.Data.Repositories;

public abstract class BaseRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _dbContext;

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<TEntity?> FindAsync(long id) =>
        await _dbContext.FindAsync<TEntity>(id);

    public virtual async Task<List<TEntity>> AllAsync() =>
        await _dbContext.Set<TEntity>().ToListAsync();

    public virtual void Create(TEntity entity) =>
        _dbContext.Add<TEntity>(entity);

    public virtual void Update(TEntity entity) =>
        _dbContext.Update<TEntity>(entity); 

    public virtual void Delete(TEntity entity) =>
        _dbContext.Set<TEntity>().Remove(entity);
}
