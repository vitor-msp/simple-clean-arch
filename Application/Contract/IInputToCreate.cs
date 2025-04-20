namespace SimpleCleanArch.Application.Contract;

public interface IInputToCreate<E>
{
    public E GetEntity();
}