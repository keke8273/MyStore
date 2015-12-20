using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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

            //Conventions
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<ProductPriceRecord> ProductPriceRecords { get; set; }
        public DbSet<ProductSource> PriceSources { get; set; }
        public DbSet<ProductOnlineAvailibility> ProductOnlineAvailibilities{ get; set; }
        public DbSet<ProductStock> ProductStocks{ get; set; }

        public T Find<T>(Guid id) where T : class
        {
            return Set<T>().Find(id);
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return this.Set<T>();
        }

        public void Save<T>(T entity) where T : class
        {
            var entry = this.Entry(entity);

            if (entry.State == EntityState.Detached)
                this.Set<T>().Add(entity);

            SaveChanges();
        }
    }
}
