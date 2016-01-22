using ParcelTracking.Contacts;
using System;

namespace ParcelTracking.Interpreters
{
    public class EmmsInterpreter : IInterpreter
    {
        private const string name = "Emms";
        private const string WarehousePhase = "澳洲仓库";
        private const string CustomPhase = "海关检验";
        private const string DeliveredPhase = "签收";
        private const string UnSuccessfulPhase = "不成功";

        public ParcelState Translate(string message)
        {
            if(String.IsNullOrEmpty(message))
                return ParcelState.Unknown;

            if(message.Contains(WarehousePhase))
                return ParcelState.Warehoused;

            if (message.Contains(CustomPhase))
                return ParcelState.AwaitCustom;

            if (message.Contains(DeliveredPhase))
                return ParcelState.Delivered;

            if (message.Contains(UnSuccessfulPhase))
                return ParcelState.Error;

            return ParcelState.InTransit;
        }

        public string Name
        {
            get { return name; }
        }
    }
}
