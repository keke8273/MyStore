using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ParcelTracking.Contacts;

namespace ParcelTracking.ReadModel
{
    public class ParcelStatus
    {
        public ParcelStatus(Guid id, Guid expressProviderId, string trackingNumber)
            : this()
        {
            TrackingNumber = trackingNumber;
            ExpressProviderId = expressProviderId;
            Id = id;
        }

        protected ParcelStatus()
        {
        }

        public Guid Id { get; private set; }
        public Guid ExpressProviderId { get; private set; }
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

        virtual public ICollection<ParcelStatusRecord> ParcelStatusHistory { get; set; }
        virtual public ExpressProvider ExpressProvider { get; set; }
    }
}
