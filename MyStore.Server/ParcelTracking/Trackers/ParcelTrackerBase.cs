using CQRS.Infrastructure.Messaging;
using ParcelTracking.ReadModel;

namespace ParcelTracking.Trackers
{
    public abstract class ParcelTrackerBase : IParcelTracker
    {
        private readonly string _name;
        private readonly IParcelStatusDao _parcelStatusDao;
        private readonly ICommandBus _commandBus;

        public ParcelTrackerBase(string name, IParcelStatusDao parcelStatusDao, ICommandBus commandBus)
        {
            _name = name;
            _parcelStatusDao = parcelStatusDao;
            _commandBus = commandBus;
        }

        public string Name { get { return _name;}}

        public abstract void Start();
        public abstract void Stop();
    }
}
