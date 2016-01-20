using System;

namespace ParcelTracking.Contacts.Events
{
    public class ParcelStatusUpdated : ParcelEvent
    {
        public ParcelState State { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Location { get; set; }

        public string Message { get; set; }

    }
}
