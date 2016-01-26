using System;
using CQRS.Infrastructure.Database;
using CQRS.Infrastructure.Messaging.Handling;
using ParcelTracking.Commands;
using ParcelTracking.Contacts.Commands;
using ParcelTracking.Interpreters;
using ParcelTracking.Trackers;

namespace ParcelTracking.Handlers
{
    public class ParcelCommandHandler 
        : ICommandHandler<CreateParcel>,
        ICommandHandler<RefreshParcelStatus>,
        ICommandHandler<UpdateParcelStatus>
    {
        private readonly Func<IDataContext<Parcel>> _contextFactory;
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
                var parcel = new Parcel(command.ParcelId, command.ExpressProvider, command.TrackingNumber);

                context.Save(parcel);

                var tracker = _trackingService.FindParcelTracker(parcel.ExpressProvider);

                tracker.TrackAsync(parcel.Id, parcel.TrackingNumber);
            }
        }

        public void Handle(RefreshParcelStatus command)
        {
            //todo::start tracker depends on the lastUpdated time. and defferent command source.
            using (var context = _contextFactory.Invoke())
            {
                var parcel = context.Find(command.ParcelId);

                var tracker = _trackingService.FindParcelTracker(parcel.ExpressProvider);

                tracker.TrackAsync(parcel.Id, parcel.TrackingNumber);

                context.Save(parcel);
            }
        }

        public void Handle(UpdateParcelStatus command)
        {
            using (var context = _contextFactory.Invoke())
            {
                var parcel = context.Find(command.ParcelId);

                var interpreter = _interpretingService.FindInterpreter(parcel.ExpressProvider);

                parcel.ProcessTrackInfo(command.TrackInfo, interpreter);

                context.Save(parcel);
            }
        }
    }
}
