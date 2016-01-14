using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParcelTracking.Parsers
{
    public class TrackDetail
    {
        public DateTime TimeStamp { get; set; }

        public string Location { get; set; }

        public string Message { get; set; }
    }
}
