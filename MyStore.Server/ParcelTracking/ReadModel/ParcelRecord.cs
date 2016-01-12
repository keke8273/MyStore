using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelTracking.ReadModel
{
    public class ParcelRecord
    {
        public Guid Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Location { get; set; }

        public string Message { get; set; }

        public Guid ParcelId { get; set; }

        virtual public Parcel {get; set;}
    }
}
