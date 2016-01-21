using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParcelTracking.Parsers;
using System.Threading.Tasks;

namespace ParcelTracking
{
    public interface IParcelTracker
    {
        Task TrackAsync(Parcel parcel);

        string Name { get; }
    }
}
