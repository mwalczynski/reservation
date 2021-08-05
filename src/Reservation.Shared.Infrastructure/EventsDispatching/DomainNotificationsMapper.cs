namespace Reservation.Shared.Infrastructure.EventsDispatching
{
    using System;

    public class DomainNotificationsMapper : IDomainNotificationsMapper
    {
        private readonly BiDictionary<string, Type> domainNotificationsMap;

        public DomainNotificationsMapper(BiDictionary<string, Type> domainNotificationsMap)
        {
            this.domainNotificationsMap = domainNotificationsMap;
        }

        public string GetName(Type type)
        {
            var result = this.domainNotificationsMap.TryGetBySecond(type, out var name) ? name : null;
            return result;
        }

        public Type GetType(string name)
        {
            var result = this.domainNotificationsMap.TryGetByFirst(name, out var type) ? type : null;
            return result;
        }
    }
}
