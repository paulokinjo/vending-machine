﻿namespace VendingMachine.Domain
{
    public abstract class ValueObject<T>
        where T : ValueObject<T>
    {
        public override bool Equals(object? obj)
        {
            var valueObject  = obj as T;

            if (ReferenceEquals(valueObject, null))
            {
                return false;
            }

            return EqualsCore(valueObject);
        }

        protected abstract bool EqualsCore(T valueObject);

        public override int GetHashCode()
        {
            return GetHashCodeCore();  
        }

        protected abstract int GetHashCodeCore();

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
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

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b) => !(a == b);
    }

}
