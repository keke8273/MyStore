using System;
using CQRS.Infrastructure.Database;
using CQRS.Infrastructure.Messaging.Handling;
using ParcelTracking.Contacts.Commands;
using ParcelTracking.Trackers;

namespace ParcelTracking.Handlers
{
    public class ParcelCommandHandler 
        : ICommandHandler<CreateParcel>,
        ICommandHandler<RefreshParcelStatus>
    {
        private Func<IDataContext<Parcel>> _contextFactory;
        private readonly ITrackingService _trackingService;

        public ParcelCommandHandler(Func<IDataContext<Parcel>> contextFactory, ITrackingService trackingService)
        {
            _contextFactory = contextFactory;
            _trackingService = trackingService;
        }

        public void Handle(CreateParcel command)
        {
            using (var context = _contextFactory.Invoke())
            {
                var parcel = new Parcel(command.ParcelId, command.ExpressProviderId, command.TrackingNumber, command.UserId);

                context.Save(parcel);
            }
        }

        public void Handle(RefreshParcelStatus command)
        {
            using (var context = _contextFactory.Invoke())
            {
                var parcel = context.Find(command.ParcelId);

                var tracker = _trackingService.FindParcelTracker(parcel.ExpressProviderId);

                //todo::make this async. tracking parcel takes a long time.
                var trackInfo = tracker.Track(parcel.TrackingNumber);

                parcel.ProcessTrackInfo(trackInfo);

                context.Save(parcel);
            }
        }
    }
}
