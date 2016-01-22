using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ParcelTracking.Contacts;

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
        public DateTime LastUpdated { get; set; }
        public string LastKnownLocation { get; set; }
        public string ChineseExpressProviderTrackingNumber { get; set; }
        public string ChineseExpressProvider { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int StateValue { get; private set; }
        
        [NotMapped]
        public ParcelState State
        {
            get { return (ParcelState)this.StateValue; }
            set{ this.StateValue = (int)value;}
        }

        public virtual ICollection<ParcelStatusRecord> ParcelStatusHistory { get; set; }
        virtual public ExpressProvider ExpressProvider { get; set; }
    }
}
