using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ParcelTracking.Parsers
{
    [DebuggerDisplay("TrackingNumber = {TrackingNumber}")]
    public class TrackInfo
    {
        public string TrackingNumber { get; set; }

        public string Destination { get; set; }

        public int ParcelCount { get; set; }

        public string ChineseExpressProvider { get; set; }

        public string ChineseExpressProviderTrackingNumber { get; set; }

        #region Equals
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TrackInfo)obj);
        }

        protected bool Equals(TrackInfo other)
        {
            return string.Equals(TrackingNumber, other.TrackingNumber) && string.Equals(Destination, other.Destination) &&
                   ParcelCount == other.ParcelCount &&
                   string.Equals(ChineseExpressProvider, other.ChineseExpressProvider) &&
                   string.Equals(ChineseExpressProviderTrackingNumber, other.ChineseExpressProviderTrackingNumber);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (TrackingNumber != null ? TrackingNumber.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Destination != null ? Destination.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ParcelCount;
                hashCode = (hashCode * 397) ^ (ChineseExpressProvider != null ? ChineseExpressProvider.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ChineseExpressProviderTrackingNumber != null ? ChineseExpressProviderTrackingNumber.GetHashCode() : 0);
                return hashCode;
            }
        }
        #endregion
    }
}
