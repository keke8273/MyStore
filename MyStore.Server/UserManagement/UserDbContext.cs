using Microsoft.AspNet.Identity.EntityFramework;

namespace UserManagement
{
    public class UserDbContext : IdentityDbContext<ApplicationUser>
    {
        public const string SchemaName = "UserManagement";

        public UserDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
    }
}
