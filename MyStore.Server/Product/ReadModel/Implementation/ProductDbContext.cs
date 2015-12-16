using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ReadModel.Implementation
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Product>().HasMany(p => p.Prices).WithRequired();
            //modelBuilder.Entity<Product>().HasRequired(p => p.Brand).WithMany();
            //modelBuilder.Entity<ProductPrice>().HasMany(p => p.PriceHistory).WithRequired();
            //modelBuilder.Entity<ProductPrice>().HasRequired(p => p.ProductSource).WithMany();
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<ProductPriceRecord> ProductPriceRecords { get; set; }
        public DbSet<ProductSource> PriceSources { get; set; }
    }
}
