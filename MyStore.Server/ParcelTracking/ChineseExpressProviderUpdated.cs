using ParcelTracking.Contacts.Events;

namespace ParcelTracking
{
    public class ChineseExpressProviderUpdated : ParcelEvent
    {
        public string ChineseExpressProvider { get; set; }
    }
}