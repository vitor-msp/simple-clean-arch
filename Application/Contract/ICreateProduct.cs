using System.ComponentModel.DataAnnotations;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;
using SimpleCleanArch.Domain.ValueObjects;

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

    public List<ProductVariant> ProductVariants = [];

    public class ProductVariant
    {
        public Color Color { get; set; }
        public Size Size { get; set; }
        public string? Description { get; set; }
    }

    public IProduct GetEntity()
    {
        var product = new Product()
        {
            Name = Name,
            Price = Price,
            Description = Description,
            Category = Category,
        };
        ProductVariants.ForEach(variant => product.AddProductVariant(
            new Domain.Entities.ProductVariant()
            {
                Color = variant.Color,
                Size = variant.Size,
                Description = variant.Description
            }));
        return product;
    }
}

public class CreateProductOutput
{
    public Guid ProductId { get; set; }
}