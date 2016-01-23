using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Subscription
{
    public class SubscriptionContext : DbContext
    {
        public const string SchemaName = "SubscriptionRegistration";

        public SubscriptionContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<global::Subscription.Subscription>().ToTable("Subscription", SchemaName);

            //Conventions
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
        }

        public virtual DbSet<Subscription> Subscriptions { get; set; }

        public global::Subscription.Subscription FindSubscription(Guid subscribeeId, Guid subscriberId)
        {
            return Subscriptions.FirstOrDefault(s => s.SubscribeeId == subscribeeId && s.SubscriberId == subscriberId);
        }
    }
}