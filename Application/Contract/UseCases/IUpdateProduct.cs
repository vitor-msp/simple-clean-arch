using SimpleCleanArch.Application.Dto;

namespace SimpleCleanArch.Application.Contract.UseCases;

public interface IUpdateProduct
{
    UpdateProductOutput? Execute(long id, UpdateProductInput input);
}