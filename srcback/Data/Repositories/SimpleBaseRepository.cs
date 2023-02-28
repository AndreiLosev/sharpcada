using Microsoft.EntityFrameworkCore;

namespace sharpcada.Data.Repositories;

public abstract class SimpleBaseRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _dbContext;

    public SimpleBaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity?> GetOneAsync(object id) =>
        await _dbContext.FindAsync<TEntity>(id);

    public async Task<ICollection<TEntity>> GetAsync() =>
        await _dbContext.Set<TEntity>().ToListAsync();

    public void Create(TEntity entity) =>
        _dbContext.Add<TEntity>(entity);

    public void Create(ICollection<TEntity> entities) =>
        _dbContext.AddRange(entities);

    public void Update(TEntity entity) =>
        _dbContext.Update<TEntity>(entity); 

    public void Update(ICollection<TEntity> entities) =>
        _dbContext.UpdateRange(entities);

    public void Delete(TEntity entity) =>
        _dbContext.Set<TEntity>().Remove(entity);

    public void Delete(ICollection<TEntity> entities) =>
        _dbContext.Set<TEntity>().RemoveRange(entities);

    public async Task SaveAsync() =>
        await _dbContext.SaveChangesAsync();
}
