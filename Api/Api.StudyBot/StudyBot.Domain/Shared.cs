namespace StudyBot.Domain;

public abstract class Entity
{
    protected Entity()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; init; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }

    public void UpdateDate()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}

public abstract class Entity<T> : Entity, IEquatable<T>
    {
        public abstract bool Equals(T? other);
        
        public override bool Equals(object? obj)
        {
            return obj is T other && Equals(other);
        }
        
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

