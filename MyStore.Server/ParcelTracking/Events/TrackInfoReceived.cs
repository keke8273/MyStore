using CQRS.Infrastructure.EventSourcing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelTracking.Events
{
    public class TrackInfoReceived : VersionedEvent
    {
    }
}
