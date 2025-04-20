using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SimpleCleanArch.Domain.ValueObjects;
using SimpleCleanArch.Repository.Schema;

namespace SimpleCleanArch.Domain.Contract;

[Table("product_variants")]
[Index(nameof(Sku), IsUnique = true)]
public class ProductVariantSchema : BaseSchema<IProductVariant, ProductVariantDto>
{
    [Key, Column("id")]
    public int Id { get; set; }

    [Column("sku")]
    public string? Sku { get; set; }

    [Column("color")]
    public Color Color { get; set; }

    [Column("size")]
    public Size Size { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [ForeignKey("Product"), Column("product_id")]
    public int ProductId { get; set; }

    public required ProductSchema Product { get; set; }

    public ProductVariantSchema() { }

    public ProductVariantSchema(IProductVariant variant)
    {
        Hydrate(variant);
    }

    public override void Update(IProductVariant variant)
    {
        Hydrate(variant);
    }

    private void Hydrate(IProductVariant variant)
    {
        Id = variant.Id;
        Sku = variant.Sku;
        CreatedAt = variant.CreatedAt;
        Color = variant.Color;
        Size = variant.Size;
        Description = variant.Description;
    }

    public override ProductVariantDto GetEntity() => new(Id, CreatedAt, Color, Size, Description, Sku);
}