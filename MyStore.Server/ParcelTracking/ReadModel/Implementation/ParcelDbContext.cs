using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelTracking.ReadModel.Implementation
{
    public class ParcelTrackingDbContext : DbContext
    {
        public const string SchemaName = "ParcelTracking";

        public ParcelTrackingDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Parcel>().ToTable("ParcelView", SchemaName);
            modelBuilder.Entity<Parcel>().HasMany(p => p.ParcelRecords).WithRequired();
            modelBuilder.Entity<ParcelRecord>().ToTable("ParcelRecord", SchemaName);
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
