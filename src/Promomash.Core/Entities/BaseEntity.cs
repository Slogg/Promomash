namespace Promomash.Core.Entities;

public abstract class BaseEntity<TId>
{
    public TId Id { get; set; }
}
