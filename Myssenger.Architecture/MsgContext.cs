using MediatR;
using Microsoft.EntityFrameworkCore;
using Myssenger.Shared;

namespace Mysennger.Architecture;

public class MsgContext : DbContext
{
    private readonly IMediator _mediator;

    protected MsgContext(IMediator mediator)
    {
        _mediator = mediator;
    }

    public MsgContext(DbContextOptions options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MsgContext).Assembly);
    }

    public override int SaveChanges()
    {
        var entities = ChangeTracker.Entries<IAggregateRoot>()
            .Select(entry => entry.Entity)
            .Where(entity => entity.Events.Any())
            .ToList();
        foreach (var entity in entities)
        {
            foreach (var domainEvent in entity.Events)
                _mediator.Publish(domainEvent);
            entity.ClearEvents();
        }
        
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var entities = ChangeTracker.Entries<IAggregateRoot>()
            .Select(entry => entry.Entity)
            .Where(entity => entity.Events.Any());
        foreach (var entity in entities)
        {
            foreach (var domainEvent in entity.Events)
                _mediator.Publish(domainEvent, cancellationToken);
            entity.ClearEvents(); 
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}