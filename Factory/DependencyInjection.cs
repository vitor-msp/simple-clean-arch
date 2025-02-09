using SimpleCleanArch.Infra;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Repository.Implementations;
using SimpleCleanArch.Repository.Database.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SimpleCleanArch.Application.CreateProduct;
using SimpleCleanArch.Application.GetProduct;
using SimpleCleanArch.Application.UpdateProduct;
using SimpleCleanArch.Application.DeleteProduct;
using SimpleCleanArch.Application.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleCleanArch.Factory;

public static class DependencyInjection
{
    public static void AddDependencyInjections(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailConfiguration>(configuration.GetSection("Mail"));

        var sqliteConnection = configuration.GetConnectionString("SqliteConnection")
            ?? throw new Exception("Missing Sqlite configuration.");
        services.AddDbContext<ProductsContext>(options => options.UseSqlite(sqliteConnection));

        services.AddScoped<ICreateProduct, CreateProduct>();
        services.AddScoped<IGetProduct, GetProduct>();
        services.AddScoped<IUpdateProduct, UpdateProduct>();
        services.AddScoped<IDeleteProduct, DeleteProduct>();

        services.AddScoped<IProductsRepository, ProductsRepositorySqlite>();

        services.AddScoped<IMailGateway, MailGateway>();
    }
}
