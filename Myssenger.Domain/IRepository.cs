using Myssenger.Application;
using Myssenger.Shared;

namespace Mysennger.Domain;

public interface IRepository<T, in TId>
    where T : AggregateRoot<TId>
    where TId : ValueObject
{
    public Task<T?> One(TId id);
    public Task<PaginatedList<T>> All(int? page, int? pageSize);

    public Task Add(T entity);
    public Task AddAll(IEnumerable<T> entities);
    
    public Task Update(TId id, T entity);
    
    public Task Remove(TId id);
    public Task RemoveAll(IEnumerable<TId> ids);
}