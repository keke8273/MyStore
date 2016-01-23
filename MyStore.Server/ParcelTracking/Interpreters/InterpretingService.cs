using System;
using System.Collections.Generic;
using System.Linq;
using ParcelTracking.ReadModel;

namespace ParcelTracking.Interpreters
{
    public class InterpretingService : IInterpretingService
    {
        private readonly IParcelStatusDao _parcelStatusDao;
        private readonly List<IInterpreter> _interpreters = new List<IInterpreter>(); 

        public InterpretingService(IParcelStatusDao parcelStatusDao)
        {
            if(parcelStatusDao == null) throw new ArgumentNullException("parcelStatusDao");

            this._parcelStatusDao = parcelStatusDao;
        }

        public IInterpreter FindInterpreter(string expressProvider)
        {
            //var expressProvider = _parcelStatusDao.FindExpressProvider(expressProviderId);

            return _interpreters.FirstOrDefault(pt => pt.Name == expressProvider);
        }

        public void RegisterInterpreter(IInterpreter interpreter)
        {
            if (_interpreters.Any(pt => pt.Name == interpreter.Name))
                throw new AggregateException(String.Format("Track {0} is already registered", interpreter.Name));

            _interpreters.Add(interpreter);
        }
    }

    public interface IInterpretingService
    {
        IInterpreter FindInterpreter(string expressProvider);

        void RegisterInterpreter(IInterpreter interpreter);
    }
}
