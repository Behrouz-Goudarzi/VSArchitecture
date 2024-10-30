using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagement.ValueObjects;
using SharedKernel.ValueObjects;

namespace ProductManagement.Infrastructure.Persistence.Configurations;

internal sealed class CategoryConfigure : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        //builder.Ignore(e => e.DomainEvents);
        ;
        builder.Property(t => t.Title)
              .HasMaxLength(200)
              .IsRequired();

        builder.HasMany(m=>m.SubCategories)
            .WithOne()
            .HasForeignKey("ParentId");
        builder.Property(t => t.Description)
            .HasMaxLength(200)
            .IsRequired(false);
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasConversion(e => e.Value, e => CategoryId.From(e))
            .UseIdentityColumn(1, 1);
    }
}
