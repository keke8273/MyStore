using System;
using System.Data.Entity;

namespace ParcelTracking.ReadModel.Implementation
{
    public class ParcelStatusDbContextInitializer : IDatabaseInitializer<ParcelStatusDbContext>
    {
        private IDatabaseInitializer<ParcelStatusDbContext> _innerInitializer;

        public ParcelStatusDbContextInitializer(IDatabaseInitializer<ParcelStatusDbContext> innerInitializer)
        {
            _innerInitializer = innerInitializer;
        }

        public void InitializeDatabase(ParcelStatusDbContext context)
        {
            _innerInitializer.InitializeDatabase(context);

            CreateExpressProviders(context);

            context.SaveChanges();
        }

        public static void CreateExpressProviders(DbContext context)
        {
            context.Set<ExpressProvider>().Add(new ExpressProvider { Id = Guid.NewGuid(), Name = "Emms" });

            context.SaveChanges();
        }
    }
}
