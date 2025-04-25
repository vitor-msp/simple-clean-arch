using System.ComponentModel.DataAnnotations;
using SimpleCleanArch.Domain;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;
using SimpleCleanArch.Domain.ValueObjects;

namespace SimpleCleanArch.Application.Contract;

public interface ICreateProduct
{
    Task<CreateProductOutput> Execute(CreateProductInput input);
}

public class CreateProductInput : IInputToCreate<IProduct>
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
        public string Color { get; set; } = "";
        public string Size { get; set; } = "";
        public string? Description { get; set; }
    }

    public IProduct GetEntity()
    {
        try
        {
            var product = new Product()
            {
                Name = Name,
                Price = Price,
                Description = Description,
                Category = Category,
            };
            ProductVariants.ForEach(variant => product.AddProductVariant(
                new ProductVariantDto(
                    Color: Enum.Parse<Color>(variant.Color, ignoreCase: true),
                    Size: Enum.Parse<Size>(variant.Size, ignoreCase: true),
                    Description: variant.Description
                )
            ));
            return product;
        }
        catch (Exception error)
        {
            throw new DomainException(error.Message);
        }
    }
}

public class CreateProductOutput
{
    public int ProductId { get; set; }
}