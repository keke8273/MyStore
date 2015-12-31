using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Store.ReadModel.Implementation;

namespace MyStore.Server.WebApi
{
    internal static class DatabaseSetup
    {
        public static void Initialize()
        {
            Database.SetInitializer<ProductDbContext>(null);
        }
    }
}