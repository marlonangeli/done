namespace Done.Domain.Shared;

public abstract class Entity : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected init; }
    public DateTime? UpdatedAt { get; protected set; }
}