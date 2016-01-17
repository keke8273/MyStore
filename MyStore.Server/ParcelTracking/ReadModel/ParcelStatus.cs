using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParcelTracking.ReadModel
{
    public class ParcelStatus
    {
        public ParcelStatus(Guid id, Guid expressProviderId, Guid userId, string trackingNumber)
        {
            TrackingNumber = trackingNumber;
            UserId = userId;
            ExpressProviderId = expressProviderId;
            Id = id;
        }

        public Guid Id { get; private set; }
        public Guid ExpressProviderId { get; private set; }
        public Guid UserId { get; private set; }
        public string TrackingNumber { get; private set; }
        public int StateValue { get; private set; }
        public DateTime LastUpdated { get; set; }
        public ICollection<ParcelStatusRecord> ParcelStatusHistory { get; set; }
        
        [NotMapped]
        public Parcel.States State
        {
            get { return (Parcel.States)this.StateValue; }
            set{ this.StateValue = (int)value;}
        }

        virtual public ExpressProvider ExpressProvider { get; set; }
    }
}
