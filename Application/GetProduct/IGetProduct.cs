using SimpleCleanArch.Application.Dto;

namespace SimpleCleanArch.Application.Contract.UseCases;

public interface IGetProduct
{
    Task<GetProductOutput?> Execute(long id);
}