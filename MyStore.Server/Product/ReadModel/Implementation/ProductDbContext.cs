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
        public const string SchemaName = "StoreManagement";

        public ProductDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Brand>().ToTable("Brand", SchemaName);
            modelBuilder.Entity<Product>().ToTable("ProductView", SchemaName);
            
            modelBuilder.Entity<ProductPrice>().ToTable("ProductPrice", SchemaName);
            modelBuilder.Entity<ProductPrice>().HasKey(p => new {p.ProductId, p.ProductSourceId});
            //modelBuilder.Entity<ProductPrice>()
            //    .HasRequired(p => p.Product)
            //    .WithMany(p => p.Prices)
            //    .HasForeignKey(p => new {p.ProductId, p.ProductSourceId});

            modelBuilder.Entity<ProductPriceRecord>().ToTable("ProductPriceRecord", SchemaName);
            modelBuilder.Entity<ProductSource>().ToTable("ProductSource", SchemaName);
            modelBuilder.Entity<ProductOnlineAvailibility>().ToTable("ProductOnlineAvailibility", SchemaName);
            modelBuilder.Entity<ProductOnlineAvailibility>().HasKey(p => new { p.ProductId, p.ProductSourceId });
            //modelBuilder.Entity<ProductOnlineAvailibility>()
            //    .HasRequired(a => a.Product)
            //    .WithMany(p => p.OnlineAvailibilities)
            //    .HasForeignKey(p => new {p.ProductId, p.ProductSourceId});

            modelBuilder.Entity<ProductStock>().ToTable("ProductStock", SchemaName);
            modelBuilder.Entity<ProductStock>().HasKey(s => new { s.ProductId, s.StockLocationId });
            //modelBuilder.Entity<ProductStock>()
            //    .HasRequired(s => s.Product)
            //    .WithMany(p => p.Stocks)
            //    .HasForeignKey(s => new {s.ProductId, s.StockLocationId});

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
