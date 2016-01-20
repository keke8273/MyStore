using CQRS.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelTracking.Trackers
{
    public abstract class BaseParcelTracker : IParcelTracker
    {
        protected ICommandBus _commandBus;

        protected BaseParcelTracker(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public abstract Task TrackAsync(Parcel parcel);
        public abstract string GetName();
    }
}
