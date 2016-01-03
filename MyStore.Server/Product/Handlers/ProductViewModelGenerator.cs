using System;
using System.Diagnostics;
using System.Linq;
using CQRS.Infrastructure.Messaging.Handling;
using CQRS.Infrastructure.Utils;
using Store.Contracts;
using Store.ReadModel;
using Store.ReadModel.Implementation;

namespace Store.Handlers
{
    public class ProductViewModelGenerator : 
        IEventHandler<ProductCreated>,
        IEventHandler<PriceUpdated>,
        IEventHandler<OnlineAvailabilityUpdated>
    {
        //use the context Factory for easy unit test setup.
        private readonly Func<ProductDbContext> _contextFactory;

        public ProductViewModelGenerator(Func<ProductDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void Handle(ProductCreated @event)
        {
            using (var repository = _contextFactory.Invoke())
            {
                var dto = repository.Find<ReadModel.Product>(@event.SourceId);
                if (dto != null)
                {
                    Trace.TraceWarning(
                        "Ignoring ProductCreated event for product with ID {0} as it was already created.",
                        @event.SourceId
                        );
                }
                else
                {
                    repository.Set<ReadModel.Product>().Add(
                        new ReadModel.Product(@event.SourceId, @event.Brand, @event.Name, @event.ImageUrl)
                        );

                    repository.SaveChanges();
                }
            }
        }

        public void Handle(PriceUpdated @event)
        {
            using (var repository = _contextFactory.Invoke())
            {
                var dto = repository.Find<ReadModel.Product>(@event.SourceId);
                if (dto != null)
                {
                    var price = dto.Prices.FirstOrDefault(p => p.ProductSourceId == @event.ProductSourceId);
                    if (price == null)
                    {
                        price =
                            new ReadModel.Price
                            {
                                Value = @event.NewPrice,
                                LastUpdated = @event.TimeStamp,
                                ProductId = @event.SourceId,
                                ProductSourceId = @event.ProductSourceId
                            };
                        dto.Prices.Add(price);
                    }
                    else
                    {
                        price.Value = @event.NewPrice;
                        price.LastUpdated = @event.TimeStamp; 
                    }

                    price.PriceHistory.Add(new PriceRecord
                    {
                        Id = GuidUtil.NewSequentialId(),
                        Value = @event.NewPrice,
                        TimeStamp = @event.TimeStamp,
                        ProductId = @event.SourceId,
                        ProductSourceId = @event.ProductSourceId
                    });

                    repository.SaveChanges();
                }
                else
                {
                    Trace.TraceWarning(
                        "Product with ID {0} does not exist",
                        @event.SourceId
                        );
                }
            }
        }

        public void Handle(OnlineAvailabilityUpdated @event)
        {
            using (var repository = _contextFactory.Invoke())
            {
                var dto = repository.Find<ReadModel.Product>(@event.SourceId);
                if (dto != null)
                {
                    var availability = dto.OnlineAvailibilities.FirstOrDefault(p => p.ProductSourceId == @event.ProductSourceId);
                    if (availability == null)
                    {
                        availability =
                            new ReadModel.OnlineAvailability()
                            {
                                IsAvailable = @event.NewAvailability,
                                LastUpdated = @event.TimeStamp,
                                ProductId = @event.SourceId,
                                ProductSourceId = @event.ProductSourceId
                            };
                        dto.OnlineAvailibilities.Add(availability);
                    }
                    else
                    {
                        availability.IsAvailable= @event.NewAvailability;
                        availability.LastUpdated = @event.TimeStamp;
                    }

                    availability.OnlineAvailabilityHistory.Add(new OnlineAvailabilityRecord
                    {
                        Id = GuidUtil.NewSequentialId(),
                        IsAvailable= @event.NewAvailability,
                        TimeStamp = @event.TimeStamp,
                        ProductId = @event.SourceId,
                        ProductSourceId = @event.ProductSourceId
                    });

                    repository.SaveChanges();
                }
                else
                {
                    Trace.TraceWarning(
                        "Product with ID {0} does not exist",
                        @event.SourceId
                        );
                }
            }
        }
    }
}
