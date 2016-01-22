using System;

namespace ParcelTracking.Contacts.Commands
{
    public class RefreshParcelStatus : ParcelCommand
    {
        public enum RefreshRequester 
        {
            User,
            ParcelTrackProcessor,
        }

        public RefreshParcelStatus(Guid parcelId) : base(parcelId)
        {
            public RefreshRequester Requester { get; set; }
        }
    }
}
