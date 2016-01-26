using System;
using CQRS.Infrastructure.Utils;

namespace ParcelTracking.ReadModel
{
    public class ParcelStatusRecord
    {
        public ParcelStatusRecord()
        {
            Id = GuidUtil.NewSequentialId();
        }

        public Guid Id { get; private set; }
        public DateTime TimeStamp { get; set; }
        public string Location { get; set; }
        public string Message { get; set; }
        public Guid ParcelStatusId { get; set; }
        virtual public ParcelStatus ParcelStatus {get; set;}
    }
}
