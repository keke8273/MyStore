using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using CQRS.Infrastructure.Sql.EventSourcing;
using CQRS.Infrastructure.Sql.MessageLog;
using CQRS.Infrastructure.Sql.Messaging.Implementation;
using Store.ReadModel.Implementation;

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

            var contexts =
                new DbContext[]
                {
                    new EventStoreDbContext(connectionString),
                    new MessageLogDbContext(connectionString),
                };

            foreach (var context in contexts)
            {
                var adapter = (IObjectContextAdapter) context;

                var script = adapter.ObjectContext.CreateDatabaseScript();
                context.Database.ExecuteSqlCommand(script);
                context.Dispose();
            }

            MessagingDbInitializer.CreateDatabaseObjects(connectionString, "SqlBus");
        }
    }
}
