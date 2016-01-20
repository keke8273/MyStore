using System;
using CQRS.Infrastructure.Database;
using CQRS.Infrastructure.Messaging.Handling;
using ParcelTracking.Contacts.Commands;
using ParcelTracking.Trackers;

namespace ParcelTracking.Handlers
{
    public class ParcelCommandHandler 
        : ICommandHandler<CreateParcel>,
        ICommandHandler<RefreshParcelStatus>,
        ICommandHandler<UpdateParcelStatus>
    {
        private Func<IDataContext<Parcel>> _contextFactory;
        private readonly ITrackingService _trackingService;
        private readonly IInterpretingService _interpretingService;

        public ParcelCommandHandler(Func<IDataContext<Parcel>> contextFactory, ITrackingService trackingService, IInterpretingService interpretingService)
        {
            _contextFactory = contextFactory;
            _trackingService = trackingService;
            _interpretingService = interpretingService;
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

                var trackInfo = tracker.TrackAsync(parcel.TrackingNumber);

                context.Save(parcel);
            }
        }

        public void Handle(UpdateParcelStatus command)
        {
            using (var context = _contextFactory.Invoke())
            {
                var parcel = context.Find(command.ParcelId);

                var interpreter = _interpretingService.FindInterpreter(parcel.ExpressProviderId);

                parcel.UpdateParcelStatus(command.TrackInfo, interpreter);

                context.Save(parcel);
            }
        }
    }
}
