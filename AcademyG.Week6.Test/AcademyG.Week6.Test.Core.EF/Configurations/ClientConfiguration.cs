using AcademyG.Week6.Test.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyG.Week6.Test.Core.EF.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ClientCode)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(50);

            // vincolo unique per il client code
            builder.HasIndex(c => c.ClientCode)
                .IsUnique();

            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Client);

        }
    }
}
