using System.ComponentModel.DataAnnotations;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.ValueObjects;

namespace SimpleCleanArch.Application.Contract;

public interface IUpdateProduct
{
    Task Execute(int id, UpdateProductInput input);
}

public class UpdateProductInput
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
        public Color Color { get; set; }

        [Required]
        public Size Size { get; set; }

        [Required]
        public string Description { get; set; } = "";
    }

    public void Update(IProduct product)
    {
        if (Price is not null)
            product.Price = (double)Price;
        product.Description = Description;
        product.Category = Category;
        if (ProductVariants.Count < 0) return;
        var newVariants = ProductVariants.Select(variant
            => new ProductVariantDto(
                Color: variant.Color,
                Size: variant.Size,
                Description: variant.Description,
                Sku: variant.Sku
            )
        );
        product.UpdateProductVariants(newVariants.ToList());
    }
}