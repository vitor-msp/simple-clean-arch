namespace SimpleCleanArch.Tests.Domain;

public class ProductTest
{
    private readonly long _id = DateTime.Now.Ticks / 1000000;
    private readonly DateTime _createdAt = DateTime.Now;
    private readonly string _name = "my product";
    private readonly double _price = 10.60;
    private readonly string _description = "my product Description";
    private readonly string _category = "category";

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
            category: _category
        );
        Assert.Equal(_id, product.Id);
        Assert.Equal(_createdAt, product.CreatedAt);
        Assert.Equal(_name, product.Name);
        Assert.Equal(_price, product.Price);
        Assert.Equal(_description, product.Description);
        Assert.Equal(_category, product.Category);
    }
}