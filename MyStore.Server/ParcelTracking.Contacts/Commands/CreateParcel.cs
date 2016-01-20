using System;

namespace ParcelTracking.Contacts.Commands
{
    public class CreateParcel : ParcelCommand 
    {
        public CreateParcel(Guid parcelId) : base(parcelId)
        {
        }

        public Guid ExpressProviderId { get; set; }
        public string TrackingNumber { get; set; }
        public Guid UserId { get; set; }
    }
}
