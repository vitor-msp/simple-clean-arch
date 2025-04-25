using Microsoft.EntityFrameworkCore;
using SimpleCleanArch.Repository.Context;
using SimpleCleanArch.Repository.Schema;

namespace Query;

public class ProductQuery(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<ProductSchema?> GetById(int id)
        => await _context.Products.AsNoTracking().FirstOrDefaultAsync(product => product.Id == id);
}
