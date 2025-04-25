using System.ComponentModel.DataAnnotations;
using SimpleCleanArch.Domain;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.ValueObjects;

namespace SimpleCleanArch.Application.Contract;

public interface IUpdateProduct
{
    Task Execute(int id, UpdateProductInput input);
}

public class UpdateProductInput : IInputToUpdate<IProduct>
{
    [Range(0.0, 100.0)]
    public double? Price { get; set; }

    [MaxLength(100)]
    public string? Description { get; set; }

    [MaxLength(10)]
    public string? Category { get; set; }

    public List<ProductVariant> ProductVariants { get; set; } = [];

    public class ProductVariant
    {
        public string? Sku { get; set; }

        [Required]
        public string Color { get; set; } = "";

        [Required]
        public string Size { get; set; } = "";

        [Required]
        public string Description { get; set; } = "";
    }

    public void Update(IProduct product)
    {
        try
        {
            if (Price is not null)
                product.Price = (double)Price;
            if (Description is not null)
                product.Description = Description;
            if (Category is not null)
                product.Category = Category;
            if (ProductVariants.Count < 0) return;
            var newVariants = ProductVariants.Select(variant
                => new ProductVariantDto(
                    Color: Enum.Parse<Color>(variant.Color, ignoreCase: true),
                    Size: Enum.Parse<Size>(variant.Size, ignoreCase: true),
                    Description: variant.Description,
                    Sku: variant.Sku
                )
            );
            product.UpdateProductVariants(newVariants.ToList());
        }
        catch (Exception error)
        {
            throw new DomainException(error.Message);
        }
    }
}