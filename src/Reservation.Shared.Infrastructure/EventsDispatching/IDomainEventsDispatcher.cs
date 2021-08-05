namespace Reservation.Shared.Infrastructure.EventsDispatching
{
    using System.Threading.Tasks;

    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}
