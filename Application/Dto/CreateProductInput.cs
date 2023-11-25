using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Application.Dto;

public class CreateProductInput
{
    public ProductFields Product { get; set; }
}