using System;

namespace ParcelTracking.Contacts.Events
{
    public class ParcelCreated : ParcelEvent
    {
        public Guid ExpressProviderId { get; set; }
        public string TrackingNumber { get; set; }
        public Guid UserId { get; set; }
    }
}
