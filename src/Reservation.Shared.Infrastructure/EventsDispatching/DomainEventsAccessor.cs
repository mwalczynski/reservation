namespace Reservation.Shared.Infrastructure.EventsDispatching
{
    using Domain;
    using Domain.Events;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class DomainEventsAccessor : IDomainEventsAccessor
    {
        private readonly DbContext context;

        public DomainEventsAccessor(DbContext context)
        {
            this.context = context;
        }

        public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
        {
            var domainEntities = this.context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            return domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();
        }

        public void ClearAllDomainEvents()
        {
            var domainEntities = this.context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());
        }
    }
}