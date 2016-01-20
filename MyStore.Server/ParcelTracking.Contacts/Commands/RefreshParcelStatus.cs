using System;

namespace ParcelTracking.Contacts.Commands
{
    public class RefreshParcelStatus : ParcelCommand
    {
        public RefreshParcelStatus(Guid parcelId) : base(parcelId)
        {
        }
    }
}
