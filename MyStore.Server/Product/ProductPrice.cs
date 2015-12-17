using System;
using CQRS.Infrastructure.EventSourcing;

namespace Product
{
    using System.Collections.Generic;
    using Contracts;

    public class ProductPrice : EventSourced
    {
        public ProductPrice(Guid id)
            : base(id)
        {
            base.RegisterHandler<PriceChanged>(OnProductChanged);
        }

        public ProductPrice(Guid id, IEnumerable<IVersionedEvent> history)
            :this(id)
        {
            LoadFrom(history);
        }

        public void UpdatePrice(decimal price, DateTime timeStamp)
        {
            base.Update(new PriceChanged{ProductPriceId = Id, Price = price, TimeStamp = timeStamp});
        }

        private void OnProductChanged(PriceChanged e)
        {

        }
    }
}
