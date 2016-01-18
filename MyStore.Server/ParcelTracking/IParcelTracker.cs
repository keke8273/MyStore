using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParcelTracking
{
    public interface IParcelTracker
    {
        public string Track(string trackNumber);

        string Name { get; }
    }
}
