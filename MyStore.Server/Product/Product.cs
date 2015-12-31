using System;
using System.Collections.Generic;
using System.Linq;
using CQRS.Infrastructure.EventSourcing;
using MyStore.Common;
using Store.Contracts;

namespace Store
{
    public class Product : EventSourced
    {
        private readonly List<ProductPrice> _prices = new List<ProductPrice>();
        private readonly List<ProductOnlineAvailibility> _onlineAvailibilities = new List<ProductOnlineAvailibility>();

        protected Product(Guid id) 
            : base(id)
        {
            base.RegisterHandler<ProductCreated>(OnProductCreated);
            base.RegisterHandler<PriceUpdated>(OnPriceUpdated);
            base.RegisterHandler<OnlineAvailabilityUpdated>(OnOnlineAvailabilityUpdated);
        }

        public Product(Guid id, IEnumerable<IVersionedEvent> history)
            :this(id)
        {
            LoadFrom(history);  
        }

        public Product(Guid id, Guid brandId, string name, Uri imageUrl) :
            this(id)
        {
            Update(new ProductCreated
            {
                BrandId = brandId,
                Name = name,
                ImageUrl = imageUrl
            });
        }

        public void UpdatePrice(Guid productSourceId, decimal price, IDateTimeService dateTimeService)
        {
            var currentPrice = _prices.FirstOrDefault(p => p.PriceSourceId == productSourceId);

            Update(new PriceUpdated
            {
                ProductSourceId = productSourceId,
                NewPrice = price,
                PreviousPrice = currentPrice == null? 0M : currentPrice.Price,
                TimeStamp = dateTimeService.GetCurrentDateTimeUtc()
            });
        }

        public void UpdateOnlineAvailibility(Guid productSourceId, bool isAvailable, IDateTimeService dateTimeService)
        {
            var currentAvailability = _onlineAvailibilities.FirstOrDefault(o => o.PriceSourceId == productSourceId);

            Update(new OnlineAvailabilityUpdated
            {
                ProductSourceId = productSourceId,
                NewAvailability = isAvailable,
                PreviousAvailability = currentAvailability != null && currentAvailability.IsAvailable,
                TimeStamp = dateTimeService.GetCurrentDateTimeUtc()
            });
        }

        private void OnProductCreated(ProductCreated e)
        {
            //todo::do something with the event.
        }

        private void OnPriceUpdated(PriceUpdated e)
        {
            var price = _prices.FirstOrDefault(p => p.PriceSourceId == e.ProductSourceId);

            if (price == null)
                _prices.Add(new ProductPrice(e.ProductSourceId, e.NewPrice));
            else
                price.Price = e.NewPrice;
        }

        private void OnOnlineAvailabilityUpdated(OnlineAvailabilityUpdated e)
        {
            var onlineAvailability = _onlineAvailibilities.FirstOrDefault(p => p.PriceSourceId == e.ProductSourceId);

            if (onlineAvailability == null)
                _onlineAvailibilities.Add(new ProductOnlineAvailibility(e.ProductSourceId, e.NewAvailability));
            else
                onlineAvailability.IsAvailable = e.NewAvailability;
        }

    }
}
