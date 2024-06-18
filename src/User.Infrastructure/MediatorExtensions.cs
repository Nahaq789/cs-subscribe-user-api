using MediatR;
using User.API.infrastructure;
using User.Domain.seedWork;

namespace User.Infrastructure;

internal static class MediatorExtensions 
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, UserContext ctx) 
    {

        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

#pragma warning disable CS8603 // Possible null reference return.
        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();
#pragma warning restore CS8603 // Possible null reference return.

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}