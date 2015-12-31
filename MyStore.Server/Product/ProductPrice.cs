using System;

namespace Store
{
    public class ProductPrice
    {
        private Guid _priceSourceId;
        private decimal _price;

        public ProductPrice(Guid sourceId, decimal price)
        {
            _priceSourceId = sourceId;
            _price = price;
        }

        public Guid PriceSourceId
        {
            get { return _priceSourceId; } 
            set { _priceSourceId = value; }
        }

        public decimal Price
        {
            get { return _price; } 
            set { _price = value; }
        }
    }
}
