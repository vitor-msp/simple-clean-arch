using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Query.Contract;
using SimpleCleanArch.Repository.Context;
using SimpleCleanArch.Repository.Schema;

namespace Query;

public class ProductQuery(AppDbContext context) : IProductQuery
{
    private readonly AppDbContext _context = context;

    public async Task<ProductSchema?> GetById(int id)
        => await _context.Products.AsNoTracking().Include("ProductVariants")
            .FirstOrDefaultAsync(product => product.Id == id);

    public async Task<List<ProductSchema>> GetMany(GetProductsInput input)
    {
        var query = _context.Products.AsNoTracking();

        if (input.MinPrice is not null)
            query = query.Where(p => p.Price >= input.MinPrice);
        if (input.MaxPrice is not null)
            query = query.Where(p => p.Price <= input.MaxPrice);
        if (!string.IsNullOrWhiteSpace(input.Category))
            query = query.Where(p => p.Category == null ? false : p.Category.Equals(input.Category));

        query = input.OrderBy.ToLower() switch
        {
            "createdat" => Order(input.OrderAsc, query, p => p.CreatedAt),
            "name" => Order(input.OrderAsc, query, p => p.Name),
            "price" => Order(input.OrderAsc, query, p => p.Price),
            "description" => Order(input.OrderAsc, query, p => p.Description!),
            "category" => Order(input.OrderAsc, query, p => p.Category!),
            _ => Order(input.OrderAsc, query, p => p.CreatedAt),
        };

        query = query.Skip(input.Skip).Take(input.Limit);

        return await query.ToListAsync();
    }

    private static IQueryable<ProductSchema> Order(
        bool orderAsc, IQueryable<ProductSchema> query, Expression<Func<ProductSchema, object>> predicate)
        => orderAsc ? query.OrderBy(predicate) : query.OrderByDescending(predicate);
}
