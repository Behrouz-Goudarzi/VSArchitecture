namespace SharedKernel.SeedWork;

public abstract class EntityAuditable<TKey, TKeyAuditable> : Entity<TKey>, IAuditable<TKeyAuditable> 
    where TKeyAuditable : struct
    where TKey :class, IEquatable<TKey>
{
    public TKeyAuditable CreatedBy { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public TKeyAuditable? ModifiedBy { get; private set; }

    public DateTime? ModifiedOn { get; private set; }
}