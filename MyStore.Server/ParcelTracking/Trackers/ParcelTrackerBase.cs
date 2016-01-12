using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelTracking.Implementation
{
    abstract public class ParcelTrackerBase : IParcelTracker
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
    }
}
