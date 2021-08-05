namespace Reservation.Shared.Domain.Events
{
    using MediatR;
    using System;

    public interface IDomainEvent : INotification
    {
        Guid Id { get; }

        DateTime OccurredOn { get; }
    }
}
