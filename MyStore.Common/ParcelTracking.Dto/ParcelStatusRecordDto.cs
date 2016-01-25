using System;

namespace ParcelTracking.Dto
{
    public class ParcelStatusRecordDto
    {
        public DateTime TimeStamp { get; set; }
        public string Location { get; set; }
        public string Message { get; set; }
    }
}
