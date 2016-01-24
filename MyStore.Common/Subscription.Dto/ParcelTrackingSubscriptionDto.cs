namespace Subscription.Dto
{
    public class ParcelTrackingSubscriptionDto : SubscriptionDto
    {
        public string ExpressProvider { get; set; }
        public string TrackingNumber { get; set; }
    }
}
