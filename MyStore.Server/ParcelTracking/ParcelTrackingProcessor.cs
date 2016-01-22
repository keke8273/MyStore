using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private CancellationTokenSource cancellationSource;
        private readonly TimeSpan _refreshDelay = new TimeSpan(1, 0, 0);

        public ParcelTrackingProcessor(ITrackingService trackingService, IParcelStatusDao parcelStatusDao)
        {
            _trackingService = trackingService;
            _parcelStatusDao = parcelStatusDao;
        }

        public void Start()
        {
            if (cancellationSource == null)
            {
                cancellationSource = new CancellationTokenSource();
                Task.Factory.StartNew(
                    () => TrackParcels(cancellationSource.Token),
                    cancellationSource.Token,
                    TaskCreationOptions.LongRunning,
                    TaskScheduler.Current);
            }
        }

        public void Stop()
        {
            using (cancellationSource)
            {
                if (cancellationSource != null)
                {
                    cancellationSource.Cancel();
                    cancellationSource = null;
                }
            }
        }

        private void TrackParcels(CancellationToken cancellationToken)
        {
            while (!cancellationSource.IsCancellationRequested)
            {
                //todo:: should we put a sleep in there to avoid too many database read?
                var parcels = _parcelStatusDao.FindUndeliveredParcels();

                foreach (var parcel in parcels)
                {
                    if (parcel.LastUpdated - DateTimeUtil.Now > _refreshDelay)
                    {
                        var tracker = _trackingService.FindParcelTracker(parcel.ExpressProviderId);

                        tracker.TrackAsync();
                    }
                }
            }
        }
    }
}
