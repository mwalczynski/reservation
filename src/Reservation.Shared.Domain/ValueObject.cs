namespace Reservation.Shared.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            var valueObject = (ValueObject)obj;

            return this.GetEqualityComponents()
                .SequenceEqual(
                    valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return this.GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (object.ReferenceEquals(a, null) && object.ReferenceEquals(b, null))
            {
                return true;
            }
            if (object.ReferenceEquals(a, null) || object.ReferenceEquals(b, null))
            {
                return false;
            }
            return a.Equals(b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }
    }
}
