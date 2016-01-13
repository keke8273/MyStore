using System;
using CQRS.Infrastructure.Messaging.Handling;
using ProductTracking.Contracts.Events;
using ProductTracking.ReadModel;
using ProductTracking.ReadModel.Implementation;

namespace ProductTracking.Handlers
{
    public class ProductStatusViewModelGenerator :
        IEventHandler<ProductPriceUpdated>,
        IEventHandler<OnlineAvailabilityUpdated>
    {
        private readonly Func<ProductStatusDbContext> _contextFactory;

        public ProductStatusViewModelGenerator(Func<ProductStatusDbContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }

        public void Handle(ProductPriceUpdated @event)
        {
            using (var context = _contextFactory.Invoke())
            {
                context.Set<PriceRecord>().Add(
                    new PriceRecord
                    {
                        ProductId = @event.SourceId,
                        ProductSourceId = @event.ProductSourceId,
                        TimeStamp = @event.TimeStamp,
                        Value = @event.NewPrice
                    });

                context.SaveChanges();
            }
        }

        public void Handle(OnlineAvailabilityUpdated @event)
        {
            using (var context = _contextFactory.Invoke())
            {
                context.Set<OnlineAvailabilityRecord>().Add(
                    new OnlineAvailabilityRecord
                    {
                        ProductId = @event.SourceId,
                        ProductSourceId = @event.ProductSourceId,
                        TimeStamp = @event.TimeStamp,
                        IsAvailable = @event.NewAvailability
                    });

                context.SaveChanges();
            }
        }
    }
}
