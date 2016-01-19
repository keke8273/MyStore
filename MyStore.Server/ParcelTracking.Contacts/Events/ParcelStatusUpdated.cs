using CQRS.Infrastructure.EventSourcing;
using System;

namespace ParcelTracking.Contacts.Events
{
    public class ParcelStatusUpdated : ParcelEvent
    {
        public ParceState State { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Location { get; set; }

        public string Message { get; set; }

        public Guid ParcelId { get; set; }
    }
}
