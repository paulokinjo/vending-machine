namespace VendingMachine.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }


        public override bool Equals(object? obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo))
            {
                return true;
            }

            if (compareTo is null)
            {
                return false;
            }

            return Id.Equals(compareTo.Id);
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

        public static bool operator !=(Entity a, Entity b) => !(a == b);

        public override int GetHashCode() =>
           (GetType().GetHashCode() * DateTime.Now.Year) + Id.GetHashCode();

        public override string ToString() => $"{GetType().Name} [Id = {Id}]";
    }

}
