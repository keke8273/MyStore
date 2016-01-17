using System;

namespace ParcelTracking.Contacts.Commands
{
    public class CreateParcel : ParcelCommand 
    {
        public Guid ParcelId { get; set; }
        public Guid ExpressProviderId { get; set; }
        public string TrackingNumber { get; set; }
        public Guid UserId { get; set; }
    }
}
