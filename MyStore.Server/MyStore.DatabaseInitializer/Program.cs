using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using CQRS.Infrastructure.Sql.EventSourcing;
using CQRS.Infrastructure.Sql.MessageLog;
using CQRS.Infrastructure.Sql.Messaging.Implementation;
using ParcelTracking.Database;
using ParcelTracking.ReadModel;
using ParcelTracking.ReadModel.Implementation;
using ProductTracking.ReadModel.Implementation;
using Store.ReadModel.Implementation;
using Subscription;
using UserManagement;

namespace MyStore.DatabaseInitializer
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.AppSettings["defaultConnection"];

            using (var context = new ProductDbContext(connectionString))
            {
                if (context.Database.Exists())
                    context.Database.Delete();

                context.Database.Create();
            }

            Database.SetInitializer<EventStoreDbContext>(null);
            Database.SetInitializer<MessageLogDbContext>(null);
            Database.SetInitializer<ProductStatusDbContext>(null);
            Database.SetInitializer<SubscriptionDbContext>(null);
            Database.SetInitializer<ParcelStatusDbContext>(null);
            //Database.SetInitializer<ParcelTrackingProcessManagerDbContext>(null);
            Database.SetInitializer<ParcelDbContext>(null);
            Database.SetInitializer<ApplicationDbContext>(null);

            var contexts =
                new DbContext[]
                {
                    new EventStoreDbContext(connectionString),
                    new MessageLogDbContext(connectionString),
                    new ProductStatusDbContext(connectionString),
                    new SubscriptionDbContext(connectionString),
                    new ParcelStatusDbContext(connectionString),
                    //new ParcelTrackingProcessManagerDbContext(connectionString),
                    new ParcelDbContext(connectionString),
                    new ApplicationDbContext(connectionString), 
                };

            foreach (var context in contexts)
            {
                var adapter = (IObjectContextAdapter)context;

                var script = adapter.ObjectContext.CreateDatabaseScript();

                context.Database.ExecuteSqlCommand(script);

                context.Dispose();
            }

            //Seed Databases
            using (var context = new ApplicationDbContext(connectionString))
            {
                ApplicationDbContextInitializer.CreateSuperUser(context);
            }

            using (var context = new ParcelStatusDbContext(connectionString))
            {
                ParcelStatusDbContextInitializer.CreateExpressProviders(context);
            }

            MessagingDbInitializer.CreateDatabaseObjects(connectionString, "SqlBus");

        }

    }
}
