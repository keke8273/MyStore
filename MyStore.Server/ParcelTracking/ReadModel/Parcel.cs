using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelTracking.ReadModel
{
    public class Parcel
    {
        public enum States
        {
            Unknown,
            Unregistered,
            Registered,
            Warehoused,
            InTransitAutralian,
            InTransitOverseas,
            AwaitCustom,
            InTransitChina,
            Delivered,
            Error
        }

        public Guid Id { get; set; }

        public string ExpressProviderId { get; set; }

        public Guid UserId { get; set; }

        public string TrackingNumber { get; set; }

        public int StateValue { get; private set; }

        public DateTime LastUpdated { get; set; }

        public string ShipmentNumber { get; set; }

        public ICollection<ParcelRecord> ParcelRecords { get; set; }
        
        [NotMapped]
        public States State
        {
            get { return (States)this.StateValue; }
            set { this.StateValue = (int)value; }
        }

        virtual public ExpressProvider ExpressProvider { get; set; }
    }
}
