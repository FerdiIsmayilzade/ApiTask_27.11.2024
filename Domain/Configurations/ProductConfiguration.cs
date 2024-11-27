using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name)
              .HasMaxLength(50)
              .IsRequired();
            builder.Property(x => x.Description)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(x=>x.Price)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(s => s.Stock)
                   .HasDefaultValue(10)
                   .IsRequired(false);
        }
    }
}
