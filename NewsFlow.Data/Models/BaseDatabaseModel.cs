using System;
using System.ComponentModel.DataAnnotations;

namespace NewsFlow.Data.Models
{
    public abstract class BaseDatabaseModel
    {
        [Required]
        public Guid Id { get; private set; }

        protected BaseDatabaseModel()
        {
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as BaseDatabaseModel;

            if (ReferenceEquals(this, compareTo))
                return true;

            if (ReferenceEquals(null, compareTo))
                return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(BaseDatabaseModel a, BaseDatabaseModel b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(BaseDatabaseModel? a, BaseDatabaseModel? b) => !(a == b);

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

