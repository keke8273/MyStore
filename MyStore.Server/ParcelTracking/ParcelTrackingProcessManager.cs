﻿using System;
using System.Collections.Generic;
using CQRS.Infrastructure.Messaging;
using CQRS.Infrastructure.Processes;
using CQRS.Infrastructure.Utils;
using ParcelTracking.Contacts.Commands;
using Subscription.Contracts;
using ParcelTracking.Contacts.Events;

namespace ParcelTracking
{
    public class ParcelTrackingProcessManager : IProcessManager
    {
        private readonly List<Envelope<ICommand>> commands = new List<Envelope<ICommand>>();

        public ParcelTrackingProcessManager()
        {
            Id = GuidUtil.NewSequentialId();
        }
 
        public Guid Id { get; private set; }
        public bool Completed { get; private set; }
        public Guid ParcelId { get; set; }
        public IEnumerable<Envelope<ICommand>> Commands { get { return this.commands; } }

        public void Handle(ParcelSubscriptionCreated @event)
        {
            ParcelId = @event.SourceId;

            var createParcelCommand = new CreateParcel(@event.SourceId)
                {
                    ExpressProviderId = @event.ExpressProviderId,
                    TrackingNumber = @event.TrackingNumber,
                    UserId = @event.UserId
                };

            AddCommand(new Envelope<ICommand>(createParcelCommand));
        }

        public void Handle(ParcelStatusUpdated @event)
        {
            if (@event.State == Contacts.ParcelState.Delivered)
                Completed = true;
        }

        private void AddCommand<T>(T command)
            where T : ICommand
        {
            commands.Add(Envelope.Create<ICommand>(command));
        }

        private void AddCommand(Envelope<ICommand> envelope)
        {
            commands.Add(envelope);
        }
    }
}
