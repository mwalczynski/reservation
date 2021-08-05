namespace Reservation.Shared.Infrastructure.EventsDispatching
{
    using Application.Events;
    using Application.Outbox;
    using Autofac;
    using Autofac.Core;
    using Domain.Events;
    using MediatR;
    using Newtonsoft.Json;
    using Serialization;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator mediator;

        private readonly ILifetimeScope scope;

        private readonly IOutboxAccessor outboxAccessor;

        private readonly IDomainEventsAccessor domainEventsProvider;

        private readonly IDomainNotificationsMapper domainNotificationsMapper;

        public DomainEventsDispatcher(
            IMediator mediator,
            ILifetimeScope scope,
            IOutboxAccessor outboxAccessor,
            IDomainEventsAccessor domainEventsProvider,
            IDomainNotificationsMapper domainNotificationsMapper)
        {
            this.mediator = mediator;
            this.scope = scope;
            this.outboxAccessor = outboxAccessor;
            this.domainEventsProvider = domainEventsProvider;
            this.domainNotificationsMapper = domainNotificationsMapper;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEvents = this.domainEventsProvider.GetAllDomainEvents();

            var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();
            foreach (var domainEvent in domainEvents)
            {
                var domainEvenNotificationType = typeof(IDomainEventNotification<>);
                var domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
                var domainNotification = this.scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
                {
                    new NamedParameter("domainEvent", domainEvent),
                    new NamedParameter("id", domainEvent.Id)
                });

                if (domainNotification != null)
                {
                    domainEventNotifications.Add(domainNotification as IDomainEventNotification<IDomainEvent>);
                }
            }

            this.domainEventsProvider.ClearAllDomainEvents();

            foreach (var domainEvent in domainEvents)
            {
                await this.mediator.Publish(domainEvent);
            }

            foreach (var domainEventNotification in domainEventNotifications)
            {
                var type = this.domainNotificationsMapper.GetName(domainEventNotification.GetType());
                var data = JsonConvert.SerializeObject(domainEventNotification, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContractResolver()
                });

                var outboxMessage = new OutboxMessage(
                    domainEventNotification.Id,
                    domainEventNotification.DomainEvent.OccurredOn,
                    type,
                    data);

                this.outboxAccessor.Add(outboxMessage);
            }
        }
    }
}
