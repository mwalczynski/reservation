namespace Reservation.Shared.Infrastructure
{
    using EventsDispatching;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;


    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;
        private readonly IDomainEventsDispatcher domainEventsDispatcher;

        public UnitOfWork(
            DbContext context,
            IDomainEventsDispatcher domainEventsDispatcher)
        {
            this.context = context;
            this.domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default, Guid? internalCommandId = null)
        {
            await this.domainEventsDispatcher.DispatchEventsAsync();

            return await this.context.SaveChangesAsync(cancellationToken);
        }
    }
}
