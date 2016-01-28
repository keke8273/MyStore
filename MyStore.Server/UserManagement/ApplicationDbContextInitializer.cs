using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UserManagement
{
    public class ApplicationDbContextInitializer : IDatabaseInitializer<ApplicationDbContext>
    {
        private IDatabaseInitializer<ApplicationDbContext> _innerInitializer;

        public ApplicationDbContextInitializer(IDatabaseInitializer<ApplicationDbContext> innerInitializer)
        {
            _innerInitializer = innerInitializer;
        }

        public void InitializeDatabase(ApplicationDbContext context)
        {
            _innerInitializer.InitializeDatabase(context);

            CreateSuperUser(context);

            context.SaveChanges();
        }

        public static void CreateSuperUser(DbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = new ApplicationUser()
            {
                UserName = "Creator",
                Email = "ke.frank.liu@gmail.com",
                EmailConfirmed = true,
                FirstName = "Ke",
                LastName = "Liu",
                Level = 1,
                JoinDate = DateTime.Now
            };

            manager.Create(user, "LKqas0383787MyStore");
        }
    }
}
