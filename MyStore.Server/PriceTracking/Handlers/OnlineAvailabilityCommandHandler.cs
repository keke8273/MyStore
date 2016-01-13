using CQRS.Infrastructure.EventSourcing;
using CQRS.Infrastructure.Messaging.Handling;
using MyStore.Common;
using ProductTracking.Contracts.Commands;

namespace ProductTracking.Handlers
{
    public class OnlineAvailabilityCommandHandler:
        ICommandHandler<UpdateProductOnlineAvailability>
    {
        private readonly IEventSourcedRepository<ProductOnlineAvailability> _repository;
        private readonly IDateTimeService _dateTimeService;

        public OnlineAvailabilityCommandHandler(IEventSourcedRepository<ProductOnlineAvailability> repository, IDateTimeService dateTimeService)
        {
            this._repository = repository;
            _dateTimeService = dateTimeService;
        }

        public void Handle(UpdateProductOnlineAvailability command)
        {
            var availability = _repository.Find(command.ProductId);

            if (availability == null)
                availability = new ProductOnlineAvailability(command.ProductId);

            availability.UpdateOnlineAvailability(command.ProductSourceId, command.IsAvailable, _dateTimeService);

            _repository.Save(availability, command.Id.ToString());
        }
    }
}
