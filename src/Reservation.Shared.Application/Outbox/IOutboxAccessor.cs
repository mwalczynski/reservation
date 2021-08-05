namespace Reservation.Shared.Application.Outbox
{
    using System.Threading.Tasks;

    public interface IOutboxAccessor
    {
        void Add(OutboxMessage message);

        Task Save();
    }
}
