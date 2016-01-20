using ParcelTracking.Contacts.Events;

namespace ParcelTracking
{
    public class ParcelDestinationUpdated : ParcelEvent
    {
        public string Destination { get; set; }
    }
}