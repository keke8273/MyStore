using System;
using CQRS.Infrastructure.EventSourcing;

namespace Product
{
    using System.Collections.Generic;
    using Contracts;

    public class Product : EventSourced
    {
        public Product(Guid id)
            : base(id)
        {
            base.RegisterHandler<ProductCreated>(OnProductCreated);
        }

        public Product(Guid id, IEnumerable<IVersionedEvent> history)
            :this(id)
        {
            LoadFrom(history);
        }

        //protected Product()
        //{
        //    OnlineAvailibilities = new List<OnlineAvailibility>();
        //    Prices = new List<Price>();
        //    Stocks = new List<Stock>();
        //}
        public string Name { get; set; }
        public Guid BrandId { get; set; }
        public Uri ImageUri { get; set; }


        private void OnProductCreated(ProductCreated e)
        {
            this.Name = e.Name;
            this.BrandId = e.BrandId;
            this.ImageUri = ImageUri;
        }

        //public List<OnlineAvailibility> OnlineAvailibilities { get; set; }

        //public List<Price> Prices { get; set; }

        //public List<Stock> Stocks { get; set; }
    }

    public class Stock : Sourced
    {
        public int Quatity { get; set; }
    }

    public class Price : Sourced
    {
        public decimal Value { get; set; }
    }

    public class OnlineAvailibility : Sourced
    {
        /// <summary>
        /// Gets or sets a value indicating whether this product is available on the source.
        /// </summary>
        public bool IsAvailable { get; set; }
    }

    public abstract class Sourced
    {
        public Source Source { get; set; }
    }
}
