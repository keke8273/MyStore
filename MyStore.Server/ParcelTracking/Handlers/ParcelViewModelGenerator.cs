using System;
using CQRS.Infrastructure.Messaging.Handling;
using ParcelTracking.Contacts.Events;
using ParcelTracking.ReadModel;
using ParcelTracking.ReadModel.Implementation;

namespace ParcelTracking.Handlers
{
    public class ParcelViewModelGenerator : 
        IEventHandler<ParcelCreated>,
        IEventHandler<ParcelStatusUpdated>
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

        }

    }
}
