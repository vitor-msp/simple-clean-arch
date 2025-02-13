using SimpleCleanArch.Domain.ValueObjects;

namespace SimpleCleanArch.Domain.Contract;

public interface IProduct
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public string Name { get; init; }
    public double Price { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }

    public void AddProductVariant(IProductVariant variant);
    public void RemoveProductVariant(string sku);
    public IProductVariant? GetProductVariant(string sku);
    public List<IProductVariant> ListProductVariants();
    public void UpdateProductVariants(List<IProductVariant> newVariants);
}