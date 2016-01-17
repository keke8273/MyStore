using System;
using CQRS.Infrastructure.Messaging;

namespace ParcelTracking.Contacts.Commands
{
    public abstract class ParcelCommand : ICommand
    {
        public ParcelCommand()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}