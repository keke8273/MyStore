using System;
using System.Data.Entity;
using System.Linq;

namespace ParcelTracking.ReadModel.Implementation
{
    public class ParcelStatusDbContext : DbContext
    {
        public const string SchemaName = "ParcelTracking";

        public ParcelStatusDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ParcelStatus>().ToTable("ParcelView", SchemaName);
            modelBuilder.Entity<ParcelStatus>().HasMany(p => p.ParcelStatusHistory).WithRequired();
            modelBuilder.Entity<ParcelStatusRecord>().ToTable("ParcelStatusRecord", SchemaName);
        }

        public T Find<T>(Guid id) where T: class
        {
            return this.Set<T>().Find(id);
        }

        public IQueryable<T> Query<T>() where T: class
        {
            return this.Set<T>();
        }

        public void Save<T>(T entity) where T : class
        {
            var entry = this.Entry(entity);

            if (entry.State == EntityState.Detached)
                this.Set<T>().Add(entity);

            this.SaveChanges();
        }

    }
}
