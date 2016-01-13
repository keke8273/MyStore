using System;
using CQRS.Infrastructure.Messaging.Handling;
using ParcelTracking.Contacts.Events;

namespace ParcelTracking.Handlers
{
    public class ParcelViewModelGenerator : 
        IEventHandler<ParcelStatusUpdated>
    {
        private readonly Func<>

        public void Handle(ParcelStatusUpdated @event)
        {
            throw new NotImplementedException();
        }
    }
}
