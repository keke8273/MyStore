using ParcelTracking.Contacts;
using System;

namespace ParcelTracking.Interpreters
{
    public class EmmsInterpreter : IInterpreter
    {
        private const string name = "Emms";
        private const string WarehousePhase = "澳洲仓库";

        public ParcelState Translate(string message)
        {
            if(message.Contains(WarehousePhase))
                return ParcelState.Warehoused;

            return ParcelState.Unknown;
        }

        public string Name
        {
            get { return name; }
        }
    }
}
