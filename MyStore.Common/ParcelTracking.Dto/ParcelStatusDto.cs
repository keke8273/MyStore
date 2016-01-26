using System;
using System.Collections.Generic;
using System.Text;

namespace ParcelTracking.Dto
{
    public class ParcelStatusDto
    {
        public string TrackingNumber { get; set; }
        public DateTime LastUpdated { get; set; }
        public string LastKnownLocation { get; set; }
        public string ChineseExpressProviderTrackingNumber { get; set; }
        public string ChineseExpressProvider { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string State { get; set; }

        public ICollection<ParcelStatusRecordDto> ParcelStatusHistory { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(String.Format("Parcel:{0}", TrackingNumber));
            stringBuilder.AppendLine(String.Format("LastUpdated:{0:G}", LastUpdated));
            stringBuilder.AppendLine(String.Format("LastKnownLocation: {0}", LastKnownLocation));
            stringBuilder.AppendLine(String.Format("State:{0}", State));

            foreach (var record in ParcelStatusHistory)
            {
                stringBuilder.AppendLine(String.Format("{0}\t{1}\t{2}", record.TimeStamp, record.Location, record.Message));                
            }

            return stringBuilder.ToString();
        }
    }
}
