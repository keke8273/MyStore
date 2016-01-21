using System;
using CQRS.Infrastructure.EventSourcing;
using CQRS.Infrastructure.Messaging;

namespace ParcelTracking.Contacts.Events
{
    public class ParcelStatusUpdated : ParcelEvent
    {
        public ParcelState State { get; set; }
    }

    public class ParcelStatusRecordReceived : VersionedEvent
    {
        public DateTime TimeStamp { get; set; }
        public string Location { get; set; }
        public string Message { get; set; }
    }

}
