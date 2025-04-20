namespace SimpleCleanArch.Application.Contract;

public interface IInputToUpdate<E>
{
    public void Update(E entity);
}