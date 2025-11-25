using Customers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceModule
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // Configure DbContext with PostgreSQL
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

        // Register repositories
        services.AddScoped<ICustomersRepository, CustomersRepository>();
        // services.AddScoped<IOrdersRepository, OrdersRepository>();
        // services.AddScoped<IProductsRepository, ProductsRepository>();

        return services;
    }
}
