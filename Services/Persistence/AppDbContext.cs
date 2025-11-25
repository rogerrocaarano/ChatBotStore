using Common.Abstractions.Persistence;
using Customers.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class AppDbContext : DbContext, IUnitOfWork
{
    public DbSet<Customer> Customers { get; set; }
    // public DbSet<Order> Orders { get; set; }
    // public DbSet<Product> Catalogs { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
