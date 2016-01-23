using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subscription
{
    public class Subscription
    {
        public enum SubscriptionType
        {
            ParcelStatusTracking = 0,
            ProductPriceTracking = 1,
            ProductAvailabilityTracking = 2,
            ProductStockTracking = 3,
        }

        public Subscription(Guid id, SubscriptionType type, Guid subscriberId, Guid subscribeeId)
        {
            Id = id;
            Type = type;
            SubscriberId = subscriberId;
            SubscribeeId = subscribeeId;
        }

        public Guid Id { get; private set; }
        public int TypeValue { get; set; }
        public Guid SubscriberId { get; set; }
        public Guid SubscribeeId { get; set; }
        [NotMapped]
        public SubscriptionType Type
        {
            get { return (SubscriptionType) TypeValue; }
            set { TypeValue = (int) value; }
        }
    }
}
