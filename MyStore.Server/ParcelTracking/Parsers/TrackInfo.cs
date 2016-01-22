using System.Collections.Generic;
using System.Diagnostics;

namespace ParcelTracking.Parsers
{
    [DebuggerDisplay("TrackingNumber = {TrackingNumber}")]
    public class TrackInfo
    {
        public string TrackingNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string ChineseExpressProvider { get; set; }
        public string ChineseExpressProviderTrackingNumber { get; set; }
        public IEnumerable<TrackMessage> Messages { get; set; }
    }
}
