using SimpleCleanArch.Application.Dto;

namespace SimpleCleanArch.Application.Contract.UseCases;

public interface IDeleteProduct
{
    Task<DeleteProductOutput?> Execute(long id);
}