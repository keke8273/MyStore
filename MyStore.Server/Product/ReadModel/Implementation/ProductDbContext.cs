using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Store.ReadModel.Implementation
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

            modelBuilder.Entity<Product>().ToTable("ProductView", SchemaName);
            modelBuilder.Entity<Category>().ToTable("Categories", SchemaName);
            modelBuilder.Entity<Source>().ToTable("Source", SchemaName);

            modelBuilder.Entity<Product>().HasMany<Category>(p => p.Categories)
                .WithMany(c => c.Products).Map(
                    pc =>
                    {
                        pc.MapLeftKey("ProductRefId");
                        pc.MapRightKey("CategoryRefId");
                        pc.ToTable("ProductCategory", SchemaName);
                    });
            
            modelBuilder.Entity<Price>().ToTable("Price", SchemaName);
            modelBuilder.Entity<Price>().HasKey(p => new {p.ProductId, p.ProductSourceId});
            modelBuilder.Entity<PriceRecord>().ToTable("PriceRecord", SchemaName);
            //modelBuilder.Entity<Price>()
            //    .HasRequired(p => p.Product)
            //    .WithMany(p => p.Prices)
            //    .HasForeignKey(p => new {p.Id, p.Id});

            modelBuilder.Entity<OnlineAvailability>().ToTable("OnlineAvailability", SchemaName);
            modelBuilder.Entity<OnlineAvailability>().HasKey(p => new { p.ProductId, p.ProductSourceId });
            modelBuilder.Entity<OnlineAvailabilityRecord>().ToTable("OnlineAvailabilityRecord", SchemaName);
            //modelBuilder.Entity<OnlineAvailability>()
            //    .HasRequired(a => a.Product)
            //    .WithMany(p => p.OnlineAvailibilities)
            //    .HasForeignKey(p => new {p.Id, p.Id});

            modelBuilder.Entity<Stock>().ToTable("Stock", SchemaName);
            modelBuilder.Entity<Stock>().HasKey(s => new { s.ProductId, s.StockLocationId });
            //modelBuilder.Entity<Stock>()
            //    .HasRequired(s => s.Product)
            //    .WithMany(p => p.Stocks)
            //    .HasForeignKey(s => new {s.Id, s.StockLocationId});

            //Conventions
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<PriceRecord> PriceRecords { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<OnlineAvailability> OnlineAvailabilities{ get; set; }
        public DbSet<Stock> Stocks{ get; set; }

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
