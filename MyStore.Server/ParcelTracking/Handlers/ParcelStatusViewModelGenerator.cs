using System;
using System.Linq;
using CQRS.Infrastructure.Messaging.Handling;
using ParcelTracking.Contacts;
using ParcelTracking.Contacts.Events;
using ParcelTracking.ReadModel;
using ParcelTracking.ReadModel.Implementation;

namespace ParcelTracking.Handlers
{
    public class ParcelStatusViewModelGenerator : 
        IEventHandler<ParcelCreated>,
        IEventHandler<ParcelStatusUpdated>,
        IEventHandler<ChineseExpressProviderTrackingNumberUpdated>,
        IEventHandler<ChineseExpressProviderUpdated>,
        IEventHandler<ParcelOriginUpdated>,
        IEventHandler<ParcelDestinationUpdated>,
        IEventHandler<ParcelStatusRecordReceived>
    {
        private readonly Func<ParcelStatusDbContext> _contextFactory;

        public ParcelStatusViewModelGenerator(Func<ParcelStatusDbContext> contextFactory)
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
                    var expressProvider =
                        context.Query<ExpressProvider>().FirstOrDefault(ep => ep.Name == @event.ExpressProvider);

                    parcel = new ParcelStatus(@event.SourceId, expressProvider.Id, @event.TrackingNumber)
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
                var parcelStatus = context.Find<ParcelStatus>(@event.SourceId);
                if (!String.IsNullOrEmpty(@event.Location))
                    parcelStatus.LastKnownLocation = @event.Location;

                var parcelRecord = new ParcelStatusRecord
                {
                    ParcelStatusId = @event.SourceId,
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
