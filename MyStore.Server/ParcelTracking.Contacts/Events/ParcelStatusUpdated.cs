using CQRS.Infrastructure.EventSourcing;
using System;

namespace ParcelTracking.Contacts.Events
{
    public class ParcelStatusUpdated : ParcelEvent
    {
        public Guid ParcelId { get; set; }

        public Parcel.State State { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Location { get; set; }

        public string Message { get; set; }

    }
}
