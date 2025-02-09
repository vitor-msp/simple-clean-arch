namespace SimpleCleanArch.Application.GetProduct;

public interface IGetProduct
{
    Task<GetProductOutput> Execute(long id);
}