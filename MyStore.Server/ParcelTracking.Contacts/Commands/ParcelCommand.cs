using System;
using CQRS.Infrastructure.Messaging;

namespace ParcelTracking.Contacts.Commands
{
    public abstract class ParcelCommand : ICommand
    {
        public ParcelCommand(Guid parcelId)
        {
            Id = Guid.NewGuid();
            ParcelId = parcelId;
        }

        public Guid Id { get; private set; }

        public Guid ParcelId { get; private set; }
    }
}