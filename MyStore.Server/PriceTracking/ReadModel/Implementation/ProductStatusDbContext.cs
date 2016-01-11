using System;
using System.Data.Entity;
using System.Linq;

namespace ProductTracking.ReadModel.Implementation
{
    public class ProductStatusDbContext : DbContext
    {
        public const string SchemaName = "ProductTracking";

        public ProductStatusDbContext(string nameOrConnectionString) :
            base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PriceRecord>().ToTable("PriceRecord", SchemaName);
            modelBuilder.Entity<OnlineAvailabilityRecord>().ToTable("OnlineAvailabilityRecord", SchemaName);
        }

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
                Set<T>().Add(entity);

            SaveChanges();
        }
    }
}
