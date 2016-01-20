namespace ParcelTracking.Contacts
{
    public enum ParcelState
    {
        Created = 0,
        Unregistered = 1,
        Registered = 2,
        Warehoused = 3,
        InTransitAutralian = 4,
        InTransitOverseas = 5,
        AwaitCustom = 6,
        InTransitChina = 7,
        Delivered = 8,
        Error = 9
    }
}