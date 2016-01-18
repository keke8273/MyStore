using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS.Infrastructure;

namespace ParcelTracking
{
    public class ParcelTrackingProcessor: IProcessor 
    {
        private readonly List<IParcelTracker> _trackers = new List<IParcelTracker>();


        public void Start()
        {

        }

        public void Stop()
        {

        }

        public void Register(IParcelTracker parcelTracker)
        {
            if(_trackers.Any( t => t.Name == parcelTracker.Name))
                throw new ArgumentException(String.Format("The tracker for {0} is already registered", parcelTracker.Name));

            _trackers.Add(parcelTracker);
        }
    }
}
