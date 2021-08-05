namespace Reservation.Shared.Infrastructure.EventsDispatching
{
    using Domain.Events;
    using System.Collections.Generic;

    public interface IDomainEventsAccessor
    {
        IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();

        void ClearAllDomainEvents();
    }
}
