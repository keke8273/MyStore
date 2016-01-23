using System;

namespace ParcelTracking.Contacts.Events
{
    public class ParcelCreated : ParcelEvent
    {
        public string ExpressProvider { get; set; }
        public string TrackingNumber { get; set; }
        public Guid UserId { get; set; }
    }
}
