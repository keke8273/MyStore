using Microsoft.AspNet.Identity.EntityFramework;

namespace UserManagement
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public static string SchemaName = "UserManagement";

        public ApplicationDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext(SchemaName);
        }
    }
}
