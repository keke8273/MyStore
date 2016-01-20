using ParcelTracking.Contacts.Events;

namespace ParcelTracking
{
    public class ChineseExpressProviderTrackingNumberUpdated : ParcelEvent
    {
        public string ChineseExpressProviderTrackingNumber { get; set; }
    }
}