namespace Reservation.Shared.Domain
{
    using Events;
    using System;
    using System.Collections.Generic;

    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        protected object Actual => this;

        private List<IDomainEvent> domainEvents;

        public IReadOnlyCollection<IDomainEvent> DomainEvents => this.domainEvents?.AsReadOnly();

        public void ClearDomainEvents()
        {
            this.domainEvents?.Clear();
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            this.domainEvents ??= new List<IDomainEvent>();

            this.domainEvents.Add(domainEvent);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Entity;

            if (other is null)
            {
                return false;
            }

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.Actual.GetType() != other.Actual.GetType())
            {
                return false;
            }

            if (this.Id == default || other.Id == default)
            {
                return false;
            }

            return this.Id == other.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (this.Actual.GetType().ToString() + Id).GetHashCode();
        }
    }
}
