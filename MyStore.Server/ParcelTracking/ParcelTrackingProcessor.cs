using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS.Infrastructure;
using ParcelTracking.Trackers;
using System.Threading;

namespace ParcelTracking
{
    public class ParcelTrackingProcessor: IProcessor 
    {
        private readonly ITrackingService _trackingService;
        private CancellationTokenSource cancellationSource;

        public ParcelTrackingProcessor(ITrackingService trackingService)
        {
            _trackingService = trackingService;
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

        private object TrackParcels(CancellationToken cancellationToken)
        {
            while (!cancellationSource.IsCancellationRequested)
            {

            }
        }
    }
}
