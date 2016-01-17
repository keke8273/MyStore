using System;

namespace ParcelTracking.ReadModel
{
    public class ParcelStatusRecord
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Location { get; set; }
        public string Message { get; set; }
        public Guid ParcelId { get; set; }
        virtual public ParcelStatus ParcelStatus {get; set;}
    }
}
