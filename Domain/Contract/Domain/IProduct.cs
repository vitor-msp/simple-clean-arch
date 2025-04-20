using System.Text.Json.Serialization;

namespace SimpleCleanArch.Domain.Contract;

public interface IProduct
{
    public int Id { get; }
    public string Name { get; init; }
    public double Price { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }

    [JsonIgnore]
    public List<IProductVariant> ProductVariants { get; }

    public void AddProductVariant(ProductVariantDto variant);
    public void RemoveProductVariant(string sku);
    public IProductVariant? GetProductVariant(string sku);
    public void UpdateProductVariants(List<ProductVariantDto> newVariants);
}