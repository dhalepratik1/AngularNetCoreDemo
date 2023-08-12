using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
           builder.Property(P => P.Id).IsRequired();
           builder.Property(P => P.Name).IsRequired().HasMaxLength(100);
           builder.Property(P => P.Description).IsRequired().HasMaxLength(180);
           builder.Property(P => P.Price).HasColumnType("decimal(18,2)");
           builder.Property(P => P.PictureUrl).IsRequired();
           builder.HasOne(b => b.ProductBrand).WithMany()
                .HasForeignKey(p => p.ProductBrandId);
           builder.HasOne(t => t.ProductType).WithMany()
                .HasForeignKey(p => p.ProductTypeId);
        }
    }
}