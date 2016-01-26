using Microsoft.AspNet.Identity;

namespace UserManagement
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) 
            : base(store)
        {

        }
    }
}
