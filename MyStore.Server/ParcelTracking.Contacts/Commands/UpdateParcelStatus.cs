using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelTracking.Contacts.Commands
{
    public class UpdateParcelStatus : ParcelCommand
    {
        public UpdateParcelStatus(Guid parcelId, TrackInfo trackInfo)
        {
            ParcelId = parcelId;
            TrackInfo = trackInfo;
        }

        public Guid ParcelId { get; private set; }

        public TrackInfo TrackInfo{ get; private set; }
    }
}
