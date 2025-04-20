namespace SimpleCleanArch.Repository.Schema;

public interface IUpdatableSchema<I>
{
    public void Update(I entity);
}