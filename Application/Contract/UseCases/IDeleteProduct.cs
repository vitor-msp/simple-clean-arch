using SimpleCleanArch.Application.Dto;

namespace SimpleCleanArch.Application.Contract.UseCases;

public interface IDeleteProduct
{
    DeleteProductOutput? Execute(long id);
}