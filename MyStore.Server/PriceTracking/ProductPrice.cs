using System;
using System.Collections.Generic;
using CQRS.Infrastructure.EventSourcing;
using CQRS.Infrastructure.Utils;
using MyStore.Common;
using ProductTracking.Contracts.Events;

namespace ProductTracking
{
    public class ProductPrice : EventSourced
    {
        private readonly Dictionary<Guid, Decimal> _prices;

        public ProductPrice(Guid id) : base(id)
        {
            RegisterHandler<ProductPriceUpdated>(OnProductPriceUpdated);
            _prices = new Dictionary<Guid, decimal>();
        }

        public ProductPrice(Guid id, IEnumerable<IVersionedEvent> history)
            : this(id)
        {
            LoadFrom(history);
        }

        public void UpdatePrice(Guid productSourceId, decimal value)
        {
            decimal currentPrice;

            if (!_prices.TryGetValue(productSourceId, out currentPrice))
            {
                currentPrice = 0;
                _prices.Add(productSourceId, value);
            }
            else
            {
                _prices[productSourceId] = value;
            }

            Update(new ProductPriceUpdated
            {
                ProductSourceId = productSourceId,
                NewPrice = value,
                PreviousPrice = currentPrice,
                TimeStamp = DateTimeUtil.Now()
            });
        }

        private void OnProductPriceUpdated(ProductPriceUpdated @event)
        {
            decimal currentPrice;
            if (!_prices.TryGetValue(@event.ProductSourceId, out currentPrice))
            {
                currentPrice = 0;
                _prices.Add(@event.ProductSourceId, @event.NewPrice);
            }
            else
            {
                _prices[@event.ProductSourceId] = @event.NewPrice;
            }
        }
    }
}
