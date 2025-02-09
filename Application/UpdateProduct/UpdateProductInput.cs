using System.ComponentModel.DataAnnotations;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Application.UpdateProduct;

public class UpdateProductInput
{
    [Range(0.0, 100.0)]
    public double? Price { get; set; }

    [MaxLength(100)]
    public string? Description { get; set; }

    [MaxLength(10)]
    public string? Category { get; set; }

    public void Update(Product product)
    {
        if (Price is not null)
            product.Price = (double)Price;
        if (Description is not null)
            product.Description = Description;
        if (Category is not null)
            product.Category = Category;
    }
}