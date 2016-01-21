using System;

namespace ParcelTracking.Interpreters
{
    public interface IInterpretingService
    {
        IInterpreter FindInterpreter(Guid expressProviderId);

        void RegisterInterpreter(IInterpreter interpreter);
    }
}