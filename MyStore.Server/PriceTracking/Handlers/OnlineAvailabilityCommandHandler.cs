﻿using CQRS.Infrastructure.EventSourcing;
using CQRS.Infrastructure.Messaging.Handling;
using MyStore.Common;
using ProductTracking.Contracts.Commands;

namespace ProductTracking.Handlers
{
    public class OnlineAvailabilityCommandHandler:
        ICommandHandler<UpdateProductOnlineAvailability>
    {
        private readonly IEventSourcedRepository<ProductOnlineAvailability> _repository;

        public OnlineAvailabilityCommandHandler(IEventSourcedRepository<ProductOnlineAvailability> repository)
        {
            this._repository = repository;
        }

        public void Handle(UpdateProductOnlineAvailability command)
        {
            var availability = _repository.Find(command.ProductId);

            if (availability == null)
                availability = new ProductOnlineAvailability(command.ProductId);

            availability.UpdateOnlineAvailability(command.ProductSourceId, command.IsAvailable);

            _repository.Save(availability, command.Id.ToString());
        }
    }
}
