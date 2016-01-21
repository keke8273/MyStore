using System;
using CQRS.Infrastructure.Messaging.Handling;
using ParcelTracking.Contacts;
using ParcelTracking.Contacts.Events;
using ParcelTracking.ReadModel;
using ParcelTracking.ReadModel.Implementation;

namespace ParcelTracking.Handlers
{
    public class ParcelViewModelGenerator : 
        IEventHandler<ParcelCreated>,
        IEventHandler<ParcelStatusUpdated>,
        IEventHandler<ChineseExpressProviderTrackingNumberUpdated>,
        IEventHandler<ChineseExpressProviderUpdated>,
        IEventHandler<ParcelOriginUpdated>,
        IEventHandler<ParcelDestinationUpdated>,
        IEventHandler<ParcelStatusRecordReceived>
    {
        private readonly Func<ParcelStatusDbContext> _contextFactory;

        public ParcelViewModelGenerator(Func<ParcelStatusDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void Handle(ParcelCreated @event)
        {
            using (var context = _contextFactory.Invoke())
            {
                var parcel = context.Find<ParcelStatus>(@event.SourceId);

                if (parcel == null)
                {
                    parcel = new ParcelStatus(@event.SourceId, @event.ExpressProviderId, @event.UserId,
                        @event.TrackingNumber)
                    {
                        State = ParcelState.Created,
                        LastUpdated = @event.TimeStamp
                    };

                    context.Set<ParcelStatus>().Add(parcel);

                    context.SaveChanges();
                }
            }
        }

        public void Handle(ParcelStatusUpdated @event)
        {
            using (var context = _contextFactory.Invoke())
            {
                var parcel = context.Find<ParcelStatus>(@event.SourceId);

                if (parcel.StateValue < (int) @event.State)
                {
                    parcel.State = @event.State;
                    parcel.LastUpdated = @event.TimeStamp;
                }

                context.SaveChanges();
            }
        }

        public void Handle(ChineseExpressProviderTrackingNumberUpdated @event)
        {
            using (var context = _contextFactory.Invoke())
            {
                var parcel = context.Find<ParcelStatus>(@event.SourceId);

                parcel.ChineseExpressProviderTrackingNumber = @event.ChineseExpressProviderTrackingNumber;
                
                parcel.LastUpdated = @event.TimeStamp;

                context.SaveChanges();
            }
        }

        public void Handle(ChineseExpressProviderUpdated @event)
        {
            using (var context = _contextFactory.Invoke())
            {
                var parcel = context.Find<ParcelStatus>(@event.SourceId);

                parcel.ChineseExpressProvider = @event.ChineseExpressProvider;

                parcel.LastUpdated = @event.TimeStamp;

                context.SaveChanges();
            }
        }

        public void Handle(ParcelOriginUpdated @event)
        {
            using (var context = _contextFactory.Invoke())
            {
                var parcel = context.Find<ParcelStatus>(@event.SourceId);

                parcel.Origin = @event.Origin;

                parcel.LastUpdated = @event.TimeStamp;

                context.SaveChanges();
            }
        }

        public void Handle(ParcelDestinationUpdated @event)
        {
            using (var context = _contextFactory.Invoke())
            {
                var parcel = context.Find<ParcelStatus>(@event.SourceId);

                parcel.Destination = @event.Destination;

                parcel.LastUpdated = @event.TimeStamp;

                context.SaveChanges();
            }
        }

        public void Handle(ParcelStatusRecordReceived @event)
        {
            using (var context = _contextFactory.Invoke())
            {
                var parcelRecord = new ParcelStatusRecord
                {
                    ParcelId = @event.SourceId,
                    Location = @event.Location,
                    Message = @event.Message,
                    TimeStamp = @event.TimeStamp
                };

                context.Set<ParcelStatusRecord>().Add(parcelRecord);

                context.SaveChanges();
            }
        }
    }
}
