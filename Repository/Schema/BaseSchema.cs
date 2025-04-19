namespace SimpleCleanArch.Repository.Schema;

public abstract class BaseSchema<T>
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public abstract T GetEntity();
}