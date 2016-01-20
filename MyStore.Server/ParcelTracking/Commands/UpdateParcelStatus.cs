using System;
using ParcelTracking.Contacts.Commands;
using ParcelTracking.Parsers;

namespace ParcelTracking.Commands
{
    public class UpdateParcelStatus : ParcelCommand
    {
        public UpdateParcelStatus(Guid parcelId, TrackInfo trackInfo) : base(parcelId)
        {
            TrackInfo = trackInfo;
        }

        public TrackInfo TrackInfo{ get; private set; }
    }
}
