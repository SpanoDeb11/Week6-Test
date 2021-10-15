using AcademyG.Week6.Test.Core.EF.Configurations;
using AcademyG.Week6.Test.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AcademyG.Week6.Test.Core.EF
{
    public class ManagementContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ManagementContext() : base() { }

        public ManagementContext(DbContextOptions<ManagementContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json")
                                            .Build();

                string connStringSQL = config.GetConnectionString("TestWeek6");

                optionsBuilder.UseSqlServer(connStringSQL);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ClientConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
        }
    }
}
