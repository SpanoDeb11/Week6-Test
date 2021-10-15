using AcademyG.Week6.Test.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyG.Week6.Test.Core.EF.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.OrderDate)
                .IsRequired();

            builder.Property(o => o.OrderCode)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(o => o.ProductCode)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(o => o.Amount)
                .IsRequired();

            // vincolo unique su order code
            builder.HasIndex(o => o.OrderCode)
                .IsUnique();

            builder.HasOne(o => o.Client)
                .WithMany(c => c.Orders);

        }
    }
}
