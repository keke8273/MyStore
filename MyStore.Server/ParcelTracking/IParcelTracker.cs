using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParcelTracking.Parsers;

namespace ParcelTracking
{
    public interface IParcelTracker
    {
        TrackInfo Track(string trackNumber);

        string Name { get; }
    }
}
