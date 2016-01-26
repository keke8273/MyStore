using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Subscription
{
    public class SubscriptionDbContext : DbContext
    {
        public const string SchemaName = "Subscription";

        public SubscriptionDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Subscription>().ToTable("Subscription", SchemaName);

            //Conventions
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
        }

        public virtual DbSet<Subscription> Subscriptions { get; set; }

        public Subscription FindSubscription(Guid subscribeeId, Guid subscriberId)
        {
            return Subscriptions.FirstOrDefault(s => s.SubscribeeId == subscribeeId && s.SubscriberId == subscriberId);
        }
    }
}