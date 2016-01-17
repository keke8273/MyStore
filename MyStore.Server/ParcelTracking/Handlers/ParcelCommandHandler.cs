using System;
using CQRS.Infrastructure.Database;
using CQRS.Infrastructure.Messaging.Handling;
using ParcelTracking.Contacts.Commands;

namespace ParcelTracking.Handlers
{
    public class ParcelCommandHandler 
        : ICommandHandler<CreateParcel>
    {
        private Func<IDataContext<Parcel>> _contextFactory;

        public ParcelCommandHandler(Func<IDataContext<Parcel>> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void Handle(CreateParcel command)
        {
            using (var context = _contextFactory.Invoke())
            {
                var parcel = new Parcel(command.ParcelId, command.ExpressProviderId, command.TrackingNumber, command.UserId);

                context.Save(parcel);
            }
        }
    }
}
