using SimpleCleanArch.Application.Dto;

namespace SimpleCleanArch.Application.Contract.UseCases;

public interface IUpdateProduct
{
    Task<UpdateProductOutput?> Execute(long id, UpdateProductInput input);
}