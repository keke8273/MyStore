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
        public Guid Id { get; private set; }
        public Guid ExpressProviderId { get; set; }
        public string TrackingNumber { get; set; }
        public Guid UserId { get; set; }
        public int MessageReceived { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string ChineseExpressProviderTrackingNumber { get; set; }
        public string ChineseExpressProvider { get; set; }
        public IEnumerable<IEvent> Events { get { return _events; }}
     
        public void UpdateParcelStatus(TrackInfo trackInfo, IInterpreter interpreter)
        {
            if (trackInfo.Origin != Origin)
                AddEvent(new ParcelOriginUpdated
                {
                    SourceId = Id, 
                    Origin = trackInfo.Origin
                });

            if(trackInfo.Destination != Destination)
                AddEvent(new ParcelDestinationUpdated
                { 
                    SourceId = Id, 
                    Destination = trackInfo.Destination
                });

            if(trackInfo.ChineseExpressProvider != ChineseExpressProvider)
                AddEvent(new ChineseExpressProviderUpdated
                {
                    SourceId = Id,
                    ChineseExpressProvider = trackInfo.ChineseExpressProvider
                });

            if (trackInfo.ChineseExpressProviderTrackingNumber != ChineseExpressProviderTrackingNumber)
                AddEvent(new ChineseExpressProviderTrackingNumberUpdated
                {
                    SourceId = Id,
                    ChineseExpressProviderTrackingNumber = trackInfo.ChineseExpressProviderTrackingNumber
                });

            for (int i = MessageReceived; i < trackInfo.TrackDetails.Count(); i++)
            {
                var trackDetail = trackInfo.TrackDetails.ToList()[i];

                AddEvent(new ParcelStatusUpdated
                {
                    SourceId = Id,
                    Location = trackDetail.Location,
                    Message = trackDetail.Message,
                    State = interpreter.Translate(trackDetail.Message),
                    TimeStamp = trackDetail.TimeStamp
                });
            }

            MessageReceived += Events.Count(e => e is ParcelStatusUpdated);
        }
        
        protected void AddEvent(IEvent @event)
        {
            _events.Add(@event);
        }
    }
}
