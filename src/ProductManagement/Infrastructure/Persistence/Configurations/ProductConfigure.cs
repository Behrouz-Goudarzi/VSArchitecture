using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain;
using SharedKernel.ValueObjects;
using Microsoft.Data.SqlClient;
using ProductManagement.ValueObjects;

namespace ProductManagement.Infrastructure.Persistence.Configurations;

internal class ProductConfigure : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        //builder.Ignore(e => e.DomainEvents);
        builder.Property(t => t.Title)
              .HasMaxLength(200)
              .IsRequired();
        builder.HasOne(t => t.Category)
            .WithMany(t => t.Products)
            .HasForeignKey(nameof(CategoryId))
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasConversion(e => e.Value, e => ProductId.From(e))
            .UseIdentityColumn(1, 1);

    }
}