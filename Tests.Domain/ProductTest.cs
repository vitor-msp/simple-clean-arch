namespace SimpleCleanArch.Tests.Domain;

public class ProductTest
{
    private readonly long _id = DateTime.Now.Ticks / 1000000;
    private readonly DateTime _createdAt = DateTime.Now;
    private readonly string _name = "my product";
    private readonly double _price = 10.60;
    private readonly string _description = "my product Description";
    private readonly string _category = "category";
    private readonly Color _color = Color.Blue;
    private readonly Size _size = Size.Large;

    private Product GetProduct(List<IProductVariant>? variants) => Product.Rebuild(
        id: _id,
        createdAt: _createdAt,
        name: _name,
        price: _price,
        description: _description,
        category: _category,
        variants: variants ?? []
    );

    private Product GetProductWithVariant()
        => GetProduct([ProductVariant.Rebuild(
                id: _id,
                createdAt: _createdAt,
                color: _color,
                size: _size
            )]);

    [Fact]
    public void CreateProduct_Success()
    {
        var product = new Product()
        {
            Name = _name,
            Price = _price,
            Description = _description,
            Category = _category
        };
        Assert.IsType<long>(product.Id);
        Assert.IsType<DateTime>(product.CreatedAt);
    }

    [Fact]
    public void CreateProduct_InvalidPrice()
    {
        var invalidPrice = -2.75;
        Action action = () => new Product()
        {
            Name = _name,
            Price = invalidPrice,
            Description = _description,
            Category = _category
        };
        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void RebuildProduct_Success()
    {
        var product = Product.Rebuild
        (
            id: _id,
            createdAt: _createdAt,
            name: _name,
            price: _price,
            description: _description,
            category: _category,
            []
        );
        Assert.Equal(_id, product.Id);
        Assert.Equal(_createdAt, product.CreatedAt);
        Assert.Equal(_name, product.Name);
        Assert.Equal(_price, product.Price);
        Assert.Equal(_description, product.Description);
        Assert.Equal(_category, product.Category);
        Assert.Equal([], product.ListProductVariants());
    }

    [Fact]
    public void GetProductVariant_Success()
    {
        var product = GetProductWithVariant();
        var sku = "my_product-blue-large";
        var variant = product.GetProductVariant(sku);
        Assert.NotNull(variant);
        Assert.Equal(_id, variant.Id);
        Assert.Equal(_createdAt, variant.CreatedAt);
        Assert.Equal(_color, variant.Color);
        Assert.Equal(_size, variant.Size);
        Assert.Equal(product, variant.Product);
        Assert.Equal(sku, variant.Sku);
    }

    [Fact]
    public void AddProductVariant_Success()
    {
        var product = GetProductWithVariant();
        var color = Color.Red;
        var size = Size.Small;
        product.AddProductVariant(color, size);
        var sku = "my_product-red-small";
        var variant = product.GetProductVariant(sku);
        Assert.NotNull(variant);
        Assert.IsType<long>(variant.Id);
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
        product.AddProductVariant(color, size);
        var variants = product.ListProductVariants();
        Assert.Equal(2, variants.Count);
    }

    [Fact]
    public void RemoveProductVariant_Success()
    {
        var product = GetProductWithVariant();
        var sku = "my_product-blue-large";
        product.RemoveProductVariant(sku);
        var variants = product.ListProductVariants();
        Assert.Empty(variants);
    }
}