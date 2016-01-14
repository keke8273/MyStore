using CQRS.Infrastructure.Messaging.Handling;
using ProductTracking.Contracts.Events;

namespace Store.Handlers
{
    public class ProductViewModelGenerator :
        IEventHandler<ProductPriceUpdated>,
        IEventHandler<OnlineAvailabilityUpdated>
    {
        public void Handle(ProductPriceUpdated @event)
        {
            throw new System.NotImplementedException();
        }

        public void Handle(OnlineAvailabilityUpdated @event)
        {
            throw new System.NotImplementedException();
        }
    }
}
