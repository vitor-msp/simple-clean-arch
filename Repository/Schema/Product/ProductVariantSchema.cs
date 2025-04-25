using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SimpleCleanArch.Domain;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.ValueObjects;

namespace SimpleCleanArch.Repository.Schema;

[Table("product_variants")]
[Index(nameof(Sku), IsUnique = true)]
public class ProductVariantSchema : BaseSchema, IUpdatableSchema<IProductVariant>, IRegenerableSchema<ProductVariantDto>
{
    [Key, Column("id")]
    public int Id { get; set; }

    [Column("sku")]
    public string? Sku { get; set; }

    [Column("color")]
    public string Color { get; set; } = "";

    [Column("size")]
    public string Size { get; set; } = "";

    [Column("description")]
    public string? Description { get; set; }

    [ForeignKey("Product"), Column("product_id")]
    public int ProductId { get; set; }

    [JsonIgnore]
    public ProductSchema? Product { get; set; }

    public ProductVariantSchema() { }

    public ProductVariantSchema(IProductVariant variant)
    {
        Hydrate(variant);
    }

    public void Update(IProductVariant variant)
    {
        Hydrate(variant);
        base.Update();
    }

    private void Hydrate(IProductVariant variant)
    {
        Id = variant.Id;
        Sku = variant.Sku;
        Color = variant.Color.ToString();
        Size = variant.Size.ToString();
        Description = variant.Description;
    }

    public ProductVariantDto GetEntity()
    {
        try
        {
            return new ProductVariantDto(Id, Enum.Parse<Color>(Color), Enum.Parse<Size>(Size), Description, Sku);
        }
        catch (Exception error)
        {
            throw new DomainException(error.Message);
        }
    }
}