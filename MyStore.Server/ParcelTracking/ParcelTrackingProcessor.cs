using System;
using System.Threading.Tasks;
using CQRS.Infrastructure;
using ParcelTracking.Trackers;
using System.Threading;
using ParcelTracking.ReadModel;
using CQRS.Infrastructure.Utils;

namespace ParcelTracking
{
    public class ParcelTrackingProcessor : IProcessor
    {
        private readonly ITrackingService _trackingService;
        private readonly IParcelStatusDao _parcelStatusDao;
        private CancellationTokenSource _cancellationSource;
        private readonly TimeSpan _refreshDelay = new TimeSpan(1, 0, 0);
        private readonly int _pollDelayMs = 1000;

        public ParcelTrackingProcessor(ITrackingService trackingService, IParcelStatusDao parcelStatusDao)
        {
            _trackingService = trackingService;
            _parcelStatusDao = parcelStatusDao;
        }

        public void Start()
        {
            if (_cancellationSource == null)
            {
                _cancellationSource = new CancellationTokenSource();
                Task.Factory.StartNew(
                    () => TrackParcels(_cancellationSource.Token),
                    _cancellationSource.Token,
                    TaskCreationOptions.LongRunning,
                    TaskScheduler.Current);
            }
        }

        public void Stop()
        {
            using (_cancellationSource)
            {
                if (_cancellationSource != null)
                {
                    _cancellationSource.Cancel();
                    _cancellationSource = null;
                }
            }
        }

        private void TrackParcels(CancellationToken cancellationToken)
        {
            while (!_cancellationSource.IsCancellationRequested)
            {
                //todo:: should we put a sleep in there to avoid too many database read?
                var parcels = _parcelStatusDao.FindUndeliveredParcels();

                foreach (var parcel in parcels)
                {
                    if (parcel.LastUpdated - DateTimeUtil.Now > _refreshDelay)
                    {
                        var tracker = _trackingService.FindParcelTracker(parcel.ExpressProvider.Name);

                        tracker.TrackAsync(parcel.Id, parcel.TrackingNumber);
                    }
                }

                Thread.Sleep(_pollDelayMs);
            }
        }
    }
}
