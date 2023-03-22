using System.Collections.Immutable;
using MediatR;

namespace Myssenger.Shared;

public interface IAggregateRoot
{
    public IReadOnlyCollection<INotification> Events { get; }
    public void AddEvent(INotification domainEvent);
    public void ClearEvents();
}

public class AggregateRoot<TId> : Entity<TId>, IAggregateRoot 
    where TId : ValueObject
{
    private ICollection<INotification>? _events;

    protected AggregateRoot(TId id) : base(id)
    {
    }

    public IReadOnlyCollection<INotification> Events
        => ReferenceEquals(null, _events)
            ? ImmutableList<INotification>.Empty
            : _events.ToImmutableList();
    
    public void AddEvent(INotification domainEvent)
    {
        _events ??= new List<INotification>();
        _events.Add(domainEvent);
    }

    public void ClearEvents()
    {
        _events = null;
    }
}