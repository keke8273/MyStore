using System.Data.Entity;

namespace ParcelTracking.Database
{
    public class ParcelDbContext : DbContext
    {
        public const string SchemaName = "Parcel";

        public ParcelDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Parcel>().ToTable("Parcel", SchemaName);
        }

        public DbSet<Parcel> Parcels { get; set; }
    }
}
