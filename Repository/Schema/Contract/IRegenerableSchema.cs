namespace SimpleCleanArch.Repository.Schema;

public interface IRegenerableSchema<O>
{
    public O GetEntity();
}