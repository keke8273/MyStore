using System;
using System.Threading.Tasks;

namespace ParcelTracking
{
    public interface IParcelTracker
    {
        Task TrackAsync(Guid parcelId, string trackingNumber);

        string Name { get; }
    }
}
