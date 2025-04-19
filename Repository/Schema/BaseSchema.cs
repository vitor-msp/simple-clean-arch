namespace SimpleCleanArch.Repository.Schema;

public abstract class BaseSchema<I, O>
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public abstract void Update(I entity);
    public abstract O GetEntity();
}