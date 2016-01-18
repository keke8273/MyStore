using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS.Infrastructure.Database;
using CQRS.Infrastructure.Messaging;
using CQRS.Infrastructure.Utils;
using ParcelTracking.Contacts.Events;
using ParcelTracking.Parsers;

namespace ParcelTracking
{
    public class Parcel : IAggregateRoot, IEventPublisher
    {
        public enum States
        {
            Created = 0,
            Unregistered = 1,
            Registered = 2,
            Warehoused = 3,
            InTransitAutralian = 4,
            InTransitOverseas = 5,
            AwaitCustom = 6,
            InTransitChina = 7,
            Delivered = 8,
            Error = 9
        }

        private readonly List<IEvent> _events = new List<IEvent>();
 
        public Parcel(Guid id, Guid expressProviderId, string trackingNumber, Guid userId)
        {
            Id = id;
            ExpressProviderId = expressProviderId;
            TrackingNumber = trackingNumber;
            UserId = userId;

            AddEvent(new ParcelCreated
            {
                SourceId = id,
                ExpressProviderId = expressProviderId,
                TrackingNumber = trackingNumber,
                UserId = userId,
            });
        }

        public void ProcessTrackInfo(TrackInfo trackInfo)
        {
            throw new NotImplementedException();
        }

        public Guid Id { get; private set; }
        public Guid ExpressProviderId { get; set; }
        public string TrackingNumber { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<IEvent> Events { get { return _events; }}

        protected void AddEvent(IEvent @event)
        {
            _events.Add(@event);
        }
    }
}
