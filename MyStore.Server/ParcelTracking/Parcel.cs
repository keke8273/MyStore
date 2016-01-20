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

        public Guid Id { get; private set; }
        public Guid ExpressProviderId { get; set; }
        public string TrackingNumber { get; set; }
        public Guid UserId { get; set; }
        public int MessageReceived { get; set; }
        public IEnumerable<IEvent> Events { get { return _events; }}

        public void UpdateParcelStatus(TrackInfo trackInfo, IInterpreter interpreter)
        {
            if(trackInfo.Origin != Origin)
                commands.Add(new ParcelOriginUpdated(Id){Origin == trackInfo.Origin});

            if(trackInfo.Destination != Destination)
                commands.Add(new UpdateParcelDestination(parcel.Id) { Destination == trackInfo.Destination });

            if(trackInfo.ChineseExpressProvider != ChineseExpressProvider)
                commands.Add(new UpdatedChineseExpressProvider(trackInfo.ChineseExpressProvider));

            if (trackInfo.ChineseExpressProviderTrackingNumber != ChineseExpressProviderTrackingNumber)
                commands.Add(new UpdateChineseExpressProviderTrackingNumber(trackInfo.ChineseExpressProviderTrackingNumber));

            for (int i = parcel.MessageReceived; i < trackInfo.TrackDetails.Count; i++)
            {
                commands.Add(interpreter.Translate(trackInfo.TrackDetails[i].));
            }

            return commands;
        }

        protected void AddEvent(IEvent @event)
        {
            _events.Add(@event);
        }
    }
}
