using System;
using System.Diagnostics;

namespace ParcelTracking.Parsers
{
    [DebuggerDisplay("TimeStamp = {TimeStamp}, Location = {Location}, Message = {Message}" )]
    public class TrackMessage
    {
        public DateTime TimeStamp { get; set; }

        public string Location { get; set; }

        public string Message { get; set; }
    }
}
