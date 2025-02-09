namespace SimpleCleanArch.Application.CreateProduct;

public interface ICreateProduct
{
    Task<CreateProductOutput> Execute(CreateProductInput input);
}