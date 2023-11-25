namespace SimpleCleanArch.Domain.Entities;

public class Product
{
    private static readonly double _minPrice = 0;
    private static readonly double _maxPrice = 100;
    private readonly ProductFields _fields;

    public Product(ProductFields fields)
    {
        ValidatePrice(fields.Price);
        _fields = fields;
    }

    public ProductFields GetFields()
    {
        return _fields;
    }

    public void Update(ProductUpdateableFields fields)
    {
        if (fields.Price != null)
        {
            ValidatePrice((double)fields.Price);
            _fields.Price = (double)fields.Price;

        }
        if (fields.Description != null) _fields.Description = fields.Description;
        if (fields.Category != null) _fields.Category = fields.Category;
    }

    public static void ValidatePrice(double price)
    {
        if (price <= _minPrice || price > _maxPrice)
            throw new Exception("price must be greater than $0 and less than or equal $100");
    }
}