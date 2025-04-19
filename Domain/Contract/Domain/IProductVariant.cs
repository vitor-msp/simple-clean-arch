using SimpleCleanArch.Domain.ValueObjects;

namespace SimpleCleanArch.Domain.Contract;

public interface IProductVariant : ICloneable
{
    public int Id { get; }
    public DateTime CreatedAt { get; }
    public Color Color { get; init; }
    public Size Size { get; init; }
    public IProduct Product { get; init; }
    public string? Sku { get; }
    public string? Description { get; set; }

    public IProductVariant GenerateSku();
}

public record ProductVariantDto(
    int? Id = null, DateTime? CreatedAt = null, Color? Color = null,
    Size? Size = null, string? Description = null, string? Sku = null
);