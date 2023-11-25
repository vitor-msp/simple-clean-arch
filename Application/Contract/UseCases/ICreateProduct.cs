using SimpleCleanArch.Application.Dto;

namespace SimpleCleanArch.Application.Contract.UseCases;

public interface ICreateProduct
{
    CreateProductOutput Execute(CreateProductInput input);
}