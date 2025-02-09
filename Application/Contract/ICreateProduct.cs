using System.ComponentModel.DataAnnotations;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Application.Contract;

public interface ICreateProduct
{
    Task<CreateProductOutput> Execute(CreateProductInput input);
}

public class CreateProductInput
{
    [Required(ErrorMessage = "name is required")]
    [MinLength(3)]
    [MaxLength(10)]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "price is required")]
    [Range(0.0, 100.0)]
    public double Price { get; set; }

    [MaxLength(100)]
    public string? Description { get; set; }

    [MaxLength(10)]
    public string? Category { get; set; }

    public IProduct GetEntity()
        => new Product()
        {
            Name = Name,
            Price = Price,
            Description = Description,
            Category = Category,
        };
}

public class CreateProductOutput
{
    public long ProductId { get; set; }
}