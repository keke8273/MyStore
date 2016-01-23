using System;
using System.Collections.Generic;
using System.Linq;
using ParcelTracking.ReadModel;

namespace ParcelTracking.Trackers
{
    public class TrackingService : ITrackingService
    {
        private readonly IParcelStatusDao _parcelStatusDao;
        private readonly List<IParcelTracker> _trackers = new List<IParcelTracker>(); 

        public TrackingService(IParcelStatusDao parcelStatusDao)
        {
            if(parcelStatusDao == null) throw new ArgumentNullException("parcelStatusDao");

            this._parcelStatusDao = parcelStatusDao;
        }

        public IParcelTracker FindParcelTracker(string expressProvider)
        {
            //var expressProvider = _parcelStatusDao.FindExpressProvider(expressProviderId);

            return _trackers.FirstOrDefault(pt => pt.Name == expressProvider);
        }

        public void RegisterTrackers(IParcelTracker tracker)
        {
            if(_trackers.Any(pt => pt.Name == tracker.Name))
                throw new AggregateException(String.Format("Track {0} is already registered", tracker.Name));

            _trackers.Add(tracker);
        }
    }

    public interface ITrackingService
    {
        IParcelTracker FindParcelTracker(string expressProvider);

        void RegisterTrackers(IParcelTracker tracker);
    }
}
