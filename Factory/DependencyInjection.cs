using SimpleCleanArch.Infra;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SimpleCleanArch.Application.Contract;
using Microsoft.Extensions.DependencyInjection;
using SimpleCleanArch.Repository.Context;
using SimpleCleanArch.Domain.Contract.Repository;
using SimpleCleanArch.Domain.Contract.Infra;
using SimpleCleanArch.Repository.Implementation;
using SimpleCleanArch.Application;

namespace SimpleCleanArch.Factory;

public static class DependencyInjection
{
    public static void AddDependencyInjections(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailConfiguration>(configuration.GetSection("Mail"));

        var sqliteConnection = configuration.GetConnectionString("SqliteConnection")
            ?? throw new Exception("Missing Sqlite configuration.");
        services.AddDbContext<AppDbContext>(options => options.UseSqlite(sqliteConnection));

        services.AddScoped<ICreateProduct, CreateProduct>();
        services.AddScoped<IUpdateProduct, UpdateProduct>();
        services.AddScoped<IDeleteProduct, DeleteProduct>();

        services.AddScoped<IProductRepository, ProductRepositorySqlite>();

        services.AddScoped<IMailGateway, MailGateway>();
    }
}
