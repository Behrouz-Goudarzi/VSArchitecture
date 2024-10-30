namespace SharedKernel.SeedWork;


/// <summary>
/// Abstract class to be inherited by entities.
/// </summary>
/// <typeparam name="TKey">Type of aggregate root Id</typeparam>
public abstract class Entity<TKey> where TKey : class
{
   

    /// <summary>
    /// Type of entity Id
    /// </summary>
    public TKey Id { get; protected set; }

    protected Entity() { }

    /// <summary>
    /// Checks equality of object with another object.
    /// </summary>
    /// <param name="obj">other object</param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if (
            !(obj is Entity<TKey> other) ||
            GetType() != other.GetType() ||
            Id == null || other.Id == null
        )
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Id.Equals(other.Id);
    }

    public static bool operator ==(Entity<TKey> a, Entity<TKey> b)
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

    public static bool operator !=(Entity<TKey> a, Entity<TKey> b)
    {
        return !(a == b);
    }

    /// <summary>
    /// Returns object hash code.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return (GetType().ToString() + Id).GetHashCode();
    }
}
