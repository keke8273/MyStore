using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CQRS.Infrastructure.Database;
using CQRS.Infrastructure.Messaging;
using ParcelTracking.Contacts;
using ParcelTracking.Contacts.Events;
using ParcelTracking.Parsers;

namespace ParcelTracking
{
    public class Parcel : IAggregateRoot, IEventPublisher
    {
        private readonly List<IEvent> _events = new List<IEvent>();

        public Parcel(Guid id, string expressProvider, string trackingNumber)
            :this()
        {
            Id = id;
            ExpressProvider = expressProvider;
            TrackingNumber = trackingNumber;

            AddEvent(new ParcelCreated
            {
                SourceId = id,
                ExpressProvider = expressProvider,
                TrackingNumber = trackingNumber,
            });
        }

        protected Parcel()
        {
        }

        public Guid Id { get; private set; }
        public string ExpressProvider { get; set; }
        public string TrackingNumber { get; set; }
        public int MessageReceived { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string ChineseExpressProviderTrackingNumber { get; set; }
        public string ChineseExpressProvider { get; set; }
        //public DateTime LastUpdated { get; set; }
        public int StateValue { get; set; }

        [NotMapped]
        public ParcelState State
        {
            get { return (ParcelState) StateValue; }
            set { StateValue = (int) value; }
        }
        public IEnumerable<IEvent> Events { get { return _events; } }

        //public void RefreshParcelStatus()
        //{
        //    LastUpdated = DateTimeUtil.Now;
        //}

        public void ProcessTrackInfo(TrackInfo trackInfo, IInterpreter interpreter)
        {
            if (trackInfo.Origin != Origin)
                AddEvent(new ParcelOriginUpdated
                {
                    SourceId = Id,
                    Origin = trackInfo.Origin
                });

            if (trackInfo.Destination != Destination)
                AddEvent(new ParcelDestinationUpdated
                {
                    SourceId = Id,
                    Destination = trackInfo.Destination
                });

            if (trackInfo.ChineseExpressProvider != ChineseExpressProvider)
            {                
                AddEvent(new ChineseExpressProviderUpdated
                {
                    SourceId = Id,
                    ChineseExpressProvider = trackInfo.ChineseExpressProvider
                });

                AddEvent(new ParcelStatusUpdated
                {
                    SourceId = Id,
                    State = ParcelState.Registered
                });
            }

            if (trackInfo.ChineseExpressProviderTrackingNumber != ChineseExpressProviderTrackingNumber)
                AddEvent(new ChineseExpressProviderTrackingNumberUpdated
                {
                    SourceId = Id,
                    ChineseExpressProviderTrackingNumber = trackInfo.ChineseExpressProviderTrackingNumber
                });

            var newMessageCount = trackInfo.Messages.Count() - MessageReceived;

            if (newMessageCount <= 0) return;

            var newState = ParcelState.Created;

            for (int i = MessageReceived; i < trackInfo.Messages.Count(); i++)
            {
                var message = trackInfo.Messages.ToList()[i];
                newState = interpreter.Translate(message.Message);

                AddEvent(new ParcelStatusRecordReceived()
                {
                    SourceId = Id,
                    Location = message.Location,
                    Message = message.Message,
                    TimeStamp = message.TimeStamp
                });

                MessageReceived = i;
            }

            if (State < newState)
            {
                State = newState;
                AddEvent(new ParcelStatusUpdated
                {
                    SourceId = Id,
                    State = newState
                });
            }
        }

        protected void AddEvent(IEvent @event)
        {
            _events.Add(@event);
        }
    }
}
