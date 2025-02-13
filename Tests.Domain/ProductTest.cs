namespace SimpleCleanArch.Tests.Domain;

public class ProductTest
{
    private readonly Guid _id = Guid.NewGuid();
    private readonly DateTime _createdAt = DateTime.Now;

    private Product GetProduct(List<IProductVariant>? variants) => Product.Rebuild(
        id: _id,
        createdAt: _createdAt,
        name: "my product",
        price: 10.6,
        description: "my product description",
        category: "category",
        variants: variants ?? []
    );

    private Product GetProductWithVariant()
        => GetProduct([ProductVariant.Rebuild(
                id: _id,
                createdAt: _createdAt,
                color: Color.Blue,
                size: Size.Medium,
                description: null
            )]);

    [Fact]
    public void CreateProduct_Success()
    {
        var product = new Product()
        {
            Name = "name",
            Price = 16.2,
            Description = "description",
            Category = "category"
        };
        Assert.IsType<Guid>(product.Id);
        Assert.IsType<DateTime>(product.CreatedAt);
    }

    [Fact]
    public void CreateProduct_InvalidPrice()
    {
        var invalidPrice = -2.75;
        Action action = () => new Product()
        {
            Name = "product",
            Price = invalidPrice,
            Description = "description",
            Category = "category"
        };
        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void RebuildProduct_Success()
    {
        var id = Guid.NewGuid();
        var createdAt=DateTime.Now;
        var product = Product.Rebuild
        (
            id: id,
            createdAt: createdAt,
            name: "product",
            price: 15.1,
            description: "description",
            category: "category",
            []
        );
        Assert.Equal(id, product.Id);
        Assert.Equal(createdAt, product.CreatedAt);
        Assert.Equal("product", product.Name);
        Assert.Equal(15.1, product.Price);
        Assert.Equal("description", product.Description);
        Assert.Equal("category", product.Category);
        Assert.Equal([], product.ListProductVariants());
    }

    [Fact]
    public void GetProductVariant_Success()
    {
        var product = GetProductWithVariant();
        var sku = "my_product-blue-medium";
        var variant = product.GetProductVariant(sku);
        Assert.NotNull(variant);
        Assert.Equal(_id, variant.Id);
        Assert.Equal(_createdAt, variant.CreatedAt);
        Assert.Equal(Color.Blue, variant.Color);
        Assert.Equal(Size.Medium, variant.Size);
        Assert.Equal(product, variant.Product);
        Assert.Equal(sku, variant.Sku);
    }

    [Fact]
    public void AddProductVariant_Success()
    {
        var product = GetProductWithVariant();
        var color = Color.Red;
        var size = Size.Small;
        product.AddProductVariant(color, size, null);
        var sku = "my_product-red-small";
        var variant = product.GetProductVariant(sku);
        Assert.NotNull(variant);
        Assert.IsType<Guid>(variant.Id);
        Assert.IsType<DateTime>(variant.CreatedAt);
        Assert.Equal(color, variant.Color);
        Assert.Equal(size, variant.Size);
        Assert.Equal(product, variant.Product);
        Assert.Equal(sku, variant.Sku);
    }

    [Fact]
    public void ListProductVariants_Success()
    {
        var product = GetProductWithVariant();
        var color = Color.Red;
        var size = Size.Small;
        product.AddProductVariant(color, size, null);
        var variants = product.ListProductVariants();
        Assert.Equal(2, variants.Count);
    }

    [Fact]
    public void RemoveProductVariant_Success()
    {
        var product = GetProductWithVariant();
        var sku = "my_product-blue-medium";
        product.RemoveProductVariant(sku);
        var variants = product.ListProductVariants();
        Assert.Empty(variants);
    }
}