using System.Configuration;
using Product.ReadModel.Implementation;

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
        }
    }
}
