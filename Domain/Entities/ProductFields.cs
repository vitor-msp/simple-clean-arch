namespace SimpleCleanArch.Domain.Entities;

public class ProductFields
{
    private readonly long _id = DateTime.Now.Ticks / 1000000;
    public long Id { get { return _id; } }
    public string Description { get; set; } = "";
    public double Price { get; set; }
    private readonly DateTime _createdAt = DateTime.Now;
    public DateTime CreatedAt { get { return _createdAt; } }
    public string Category { get; set; } = "";

    public ProductFields() { }

    private ProductFields(long id, DateTime createdAt)
    {
        _id = id;
        _createdAt = createdAt;
    }

    public static ProductFields Rebuild(long id, string description, double price, DateTime createdAt, string category)
    {
        return new ProductFields(id, createdAt)
        {
            Description = description,
            Price = price,
            Category = category
        };
    }
}