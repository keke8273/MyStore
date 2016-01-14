using CQRS.Infrastructure.Messaging;
using ParcelTracking.ReadModel;

namespace ParcelTracking.Trackers
{
    public abstract class BaseParcelTracker : IParcelTracker
    {
        private readonly string _name;
        private readonly IParcelStatusDao _parcelStatusDao;
        private readonly ICommandBus _commandBus;

        public BaseParcelTracker(string name, IParcelStatusDao parcelStatusDao, ICommandBus commandBus)
        {
            _name = name;
            _parcelStatusDao = parcelStatusDao;
            _commandBus = commandBus;
        }


        public abstract void Start();
        public abstract void Stop();
        
        public string Name 
        {
            get { return _name; }
        }
    }
}
