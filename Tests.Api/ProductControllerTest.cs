namespace Tests.Api;

public class ProductControllerTest
{
    private readonly string _name = "my product";
    private readonly double _price = 10.69;
    private readonly string _description = "my product description";
    private readonly string _category = "category";

    private static (ProductController controller, ProductContext context) MakeSut()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var contextOptions = new DbContextOptionsBuilder<ProductContext>().UseSqlite(connection).Options;
        var context = new ProductContext(contextOptions);
        context.Database.EnsureCreatedAsync();
        var repository = new ProductRepositorySqlite(context);
        var mail = new Mock<MailGateway>().Object;
        var createProduct = new CreateProduct(repository, mail);
        var deleteProduct = new DeleteProduct(repository, mail);
        var updateProduct = new UpdateProduct(repository);
        var controller = new ProductController(createProduct, deleteProduct, updateProduct);
        return (controller, context);
    }

    [Fact]
    public async Task PostProduct_Success()
    {
        var input = new CreateProductInput()
        {
            Name = _name,
            Price = _price,
            Description = _description,
            Category = _category,
        };
        var (controller, context) = MakeSut();
        var output = await controller.Post(input);
        var outputResult = Assert.IsType<CreatedAtRouteResult>(output.Result);
        var outputContent = Assert.IsType<CreateProductOutput>(outputResult.Value);
        var productId = Assert.IsType<long>(outputContent.ProductId);
        var productSchema = await context.Products.FindAsync(productId);
        Assert.NotNull(productSchema);
        Assert.Equal(productId, productSchema.Id);
        Assert.IsType<DateTime>(productSchema.CreatedAt);
        Assert.Equal(input.Name, productSchema.Name);
        Assert.Equal(input.Price, productSchema.Price);
        Assert.Equal(input.Description, productSchema.Description);
        Assert.Equal(input.Category, productSchema.Category);
    }
}