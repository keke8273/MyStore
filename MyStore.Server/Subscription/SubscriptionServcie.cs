using System;
using CQRS.Infrastructure.Messaging;
using CQRS.Infrastructure.Utils;
using Subscription.Contracts;

namespace Subscription
{
    public class SubscriptionServcie
    {
        private readonly IEventBus _eventBus;
        private readonly string _nameOrConnectionString;

        public SubscriptionServcie(IEventBus eventBus, string nameOrConnectionString)
        {
            _eventBus = eventBus;
            _nameOrConnectionString = nameOrConnectionString;
        }

        public void CreateParcelSubscription(Guid parcelId, Guid userId, string expressProvider, string trackingNumber)
        {
            using (var context = new SubscriptionContext(_nameOrConnectionString))
            {
                var parcelSubscription = context.FindSubscription(parcelId, userId);

                if (parcelSubscription != null)
                    throw new DuplicateSubscriptionException(parcelId, userId, Subscription.SubscriptionType.ParcelStatusTracking);

                parcelSubscription = new Subscription(GuidUtil.NewSequentialId(),
                        Subscription.SubscriptionType.ParcelStatusTracking, userId, parcelId);

                context.Subscriptions.Add(parcelSubscription);

                context.SaveChanges();

                _eventBus.Publish(new ParcelSubscriptionCreated
                {
                    SourceId = parcelSubscription.Id,
                    ExpressProvider = expressProvider,
                    ParceId = parcelId,
                    TrackingNumber = trackingNumber,
                    UserId = userId
                });
            }
        }
    }

    public class DuplicateSubscriptionException : Exception
    {
        public DuplicateSubscriptionException(Guid subscribeeId, Guid userId, global::Subscription.Subscription.SubscriptionType type)
        {
        }
    }
}
