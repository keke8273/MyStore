using System;
using System.Diagnostics;
using CQRS.Infrastructure.Messaging.Handling;
using Product.Events;
using Product.ReadModel.Implementation;

namespace Product.Handlers
{
    public class ProductViewModelGenerator : IEventHandler<ProductCreated>
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
                        new ReadModel.Product(@event.SourceId, @event.BrandId, @event.ProductName, @event.ImageUri)
                        );

                    repository.SaveChanges();
                }
            }
        }
    }
}
