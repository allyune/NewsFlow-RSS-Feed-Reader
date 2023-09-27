using System;
using System.ComponentModel.DataAnnotations;

namespace NewsFlow.Data.Models
{
    public abstract class BaseDbModel
    {
        [Required]
        public Guid Id { get; protected set; }

        protected BaseDbModel()
        {
        }

        public override bool Equals(object? obj)
        {
            var compareTo = obj as BaseDbModel;

            if (ReferenceEquals(this, compareTo))
                return true;

            if (ReferenceEquals(null, compareTo))
                return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(BaseDbModel a, BaseDbModel b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(BaseDbModel? a, BaseDbModel? b) => !(a == b);

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }
    }
}

