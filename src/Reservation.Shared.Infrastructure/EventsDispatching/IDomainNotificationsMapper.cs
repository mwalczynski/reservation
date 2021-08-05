namespace Reservation.Shared.Infrastructure.EventsDispatching
{
    using System;

    public interface IDomainNotificationsMapper
    {
        string GetName(Type type);

        Type GetType(string name);
    }
}
