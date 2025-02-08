using SimpleCleanArch.Application.Dto;

namespace SimpleCleanArch.Application.Contract.UseCases;

public interface ICreateProduct
{
    Task<CreateProductOutput> Execute(CreateProductInput input);
}