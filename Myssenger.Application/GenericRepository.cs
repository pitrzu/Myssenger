using Microsoft.EntityFrameworkCore;
using Mysennger.Domain;
using Myssenger.Shared;

namespace Myssenger.Application;

public abstract class DbGenericRepository<T, TId> : IGenericRepository<T, TId>
    where T : AggregateRoot<TId>
    where TId : ValueObject
{
    protected readonly DbSet<T> DbSet;

    protected DbGenericRepository(DbContext context)
    {
        DbSet = context.Set<T>();
    }

    public async Task<T?> One(TId id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<ICollection<T>> All()
    {
        return await DbSet.ToListAsync();
    }

    public async Task Add(T entity)
    { 
        await DbSet.AddAsync(entity);
    }

    public async Task AddAll(IEnumerable<T> entities)
    {
        await DbSet.AddRangeAsync(entities);
    }

    public async Task Update(TId id, T entity)
    {
        if (entity.Id != id)
            return;
        if(!await DbSet.AnyAsync(t => t.Id == id))
            await Add(entity);

        DbSet.Update(entity);
    }

    public async Task Remove(TId id)
    {
        var tToRemove = await DbSet.FindAsync(id);
        if (ReferenceEquals(null, tToRemove))
            return;

        DbSet.Remove(tToRemove);
    }

    public Task RemoveAll(IEnumerable<TId> ids)
    {
        var toRemove = DbSet.Where(t => ids.Contains(t.Id));
        DbSet.RemoveRange(toRemove);
        
        return Task.CompletedTask;
    }
}