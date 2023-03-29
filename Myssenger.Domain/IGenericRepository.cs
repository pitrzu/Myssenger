using Myssenger.Shared;

namespace Mysennger.Domain;

public interface IGenericRepository<T, in TId>
    where T : AggregateRoot<TId>
    where TId : ValueObject
{
    public T? One(TId id);
    public ICollection<T> All();

    public T Add(T entity);
    public T Update(T entity);

    public int Remove(TId id);
}