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
}
