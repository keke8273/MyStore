using System;
using System.Collections.Generic;
using CQRS.Infrastructure.EventSourcing;
using Product.Events;

namespace Product
{
    public class Product : EventSourced
    {
        protected Product(Guid id) 
            : base(id)
        {
            base.RegisterHandler<ProductCreated>(OnProductCreated);
        }

        public Product(Guid id, IEnumerable<IVersionedEvent> history)
            :this(id)
        {
            LoadFrom(history);  
        }

        public Product(Guid id, Guid brandId, string name, Uri imageUri) :
            this(id)
        {
            Update(new ProductCreated
            {
                SourceId = id,
                BrandId = brandId,
                ProductName = name,
                ImageUri = imageUri
            });
        }

        private void OnProductCreated(ProductCreated e)
        {
            //todo::do something with the event.
        }
    }
}
