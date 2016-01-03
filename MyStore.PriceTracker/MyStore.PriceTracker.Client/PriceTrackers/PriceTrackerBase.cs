namespace MyStore.PriceTracker.Client.PriceTrackers
{
    public abstract class PriceTrackerBase
    {
        protected string SourceName { get; set; }

        public abstract void TrackPrice();
    }
}
