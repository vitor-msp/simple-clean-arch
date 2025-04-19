using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Repository.Schema;

public class ProductSchema : BaseSchema<IProduct, IProduct>
{
    public string Name { get; set; } = "";
    public double Price { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public List<ProductVariantSchema> ProductVariants { get; set; } = [];

    public ProductSchema() { }

    public ProductSchema(IProduct product)
    {
        Hydrate(product);
        CreateProductVariants(product);
    }

    public override void Update(IProduct product)
    {
        Hydrate(product);
        var variants = product.ProductVariants;
        ProductVariants = EliminateDeletedProductVariants(variants);
        UpdateProductVariants(variants);
    }

    public override IProduct GetEntity()
    {
        var variants = new List<ProductVariantDto>();
        ProductVariants.ForEach(variantSchema => variants.Add(variantSchema.GetEntity()));
        return Product.Rebuild(Id, CreatedAt, Name, Price, Description, Category, variants);
    }

    private void Hydrate(IProduct product)
    {
        Id = product.Id;
        CreatedAt = product.CreatedAt;
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