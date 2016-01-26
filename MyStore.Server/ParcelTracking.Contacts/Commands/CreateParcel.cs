using System;

namespace ParcelTracking.Contacts.Commands
{
    public class CreateParcel : ParcelCommand 
    {
        public CreateParcel(Guid parcelId) : base(parcelId)
        {
        }

        public string ExpressProvider { get; set; }
        public string TrackingNumber { get; set; }
    }
}
