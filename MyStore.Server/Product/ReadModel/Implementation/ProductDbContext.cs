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

            modelBuilder.Entity<Product>().HasMany(p => p.Prices).WithRequired();
            modelBuilder.Entity<Product>().HasRequired(p => p.Brand).WithMany();
            modelBuilder.Entity<ProductPrice>().HasMany(p => p.PriceHistory).WithRequired();
            modelBuilder.Entity<ProductPrice>().HasRequired(p => p.PriceSource).WithMany();
        }

    }
}
