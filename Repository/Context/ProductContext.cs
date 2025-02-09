using Microsoft.EntityFrameworkCore;
using SimpleCleanArch.Repository.Schema;

namespace SimpleCleanArch.Repository.Context;

public class ProductContext(DbContextOptions<ProductContext> options) : DbContext(options)
{
    public DbSet<ProductSchema> Products { get; set; }
}