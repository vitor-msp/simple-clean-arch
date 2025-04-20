using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Repository.Schema;

[Table("products")]
[Index(nameof(Name), IsUnique = true)]
public class ProductSchema : BaseSchema, IUpdatableSchema<IProduct>, IRegenerableSchema<IProduct>
{
    [Key, Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = "";

    [Column("price")]
    public double Price { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("category")]
    public string? Category { get; set; }

    public List<ProductVariantSchema> ProductVariants { get; set; } = [];

    public ProductSchema() { }

    public ProductSchema(IProduct product)
    {
        Hydrate(product);
        CreateProductVariants(product);
    }

    public void Update(IProduct product)
    {
        Hydrate(product);
        var variants = product.ProductVariants;
        ProductVariants = EliminateDeletedProductVariants(variants);
        UpdateProductVariants(variants);
        base.Update();
    }

    public IProduct GetEntity()
    {
        var variants = new List<ProductVariantDto>();
        ProductVariants.ForEach(variantSchema => variants.Add(variantSchema.GetEntity()));
        return Product.Rebuild(Id, Name, Price, Description, Category, variants);
    }

    private void Hydrate(IProduct product)
    {
        Id = product.Id;
        Name = product.Name;
        Price = product.Price;
        Description = product.Description;
        Category = product.Category;
    }

    private void CreateProductVariants(IProduct product)
        => product.ProductVariants.ForEach(variant =>
            ProductVariants.Add(new ProductVariantSchema(variant) { Product = this }));

    private List<ProductVariantSchema> EliminateDeletedProductVariants(List<IProductVariant> variants)
        => ProductVariants.Where(variantSchema =>
            variants.Any(variant => variant.Sku == variantSchema.Sku)).ToList();

    private void UpdateProductVariants(List<IProductVariant> variants)
    {
        variants.ForEach(variant =>
        {
            var variantSchema = ProductVariants.Find(variantSchema => variantSchema.Sku == variant.Sku);
            if (variantSchema is null)
                ProductVariants.Add(new ProductVariantSchema(variant) { Product = this });
            else
                variantSchema.Update(variant);
        });
    }
}