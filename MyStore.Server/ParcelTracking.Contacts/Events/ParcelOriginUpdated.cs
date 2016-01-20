using System;

namespace ParcelTracking.Contacts.Events
{
    public class ParcelOriginUpdated : ParcelEvent
    {
        public string Origin { get; set; }
    }
}