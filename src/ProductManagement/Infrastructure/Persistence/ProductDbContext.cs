using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain;
using ProductManagement.Infrastructure.Persistence.Configurations;

namespace ProductManagement.Infrastructure.Persistence;
//Add-Migration initProductDb  -OutputDir Infrastructure/Persistence/Migrations -Context ProductDbContext -StartupProject webApi
internal sealed class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ApplyConfiguration(new ProductConfigure());
        builder.ApplyConfiguration(new CategoryConfigure());


        base.OnModelCreating(builder);
    }


}


