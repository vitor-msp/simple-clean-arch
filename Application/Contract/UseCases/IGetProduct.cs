using SimpleCleanArch.Application.Dto;

namespace SimpleCleanArch.Application.Contract.UseCases;

public interface IGetProduct
{
    GetProductOutput? Execute(long id);
}