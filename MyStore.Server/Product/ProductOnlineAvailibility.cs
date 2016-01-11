using System;

namespace Store
{
    public class ProductOnlineAvailability
    {
        private Guid _priceSourceId;
        private bool _isAvailable;

        public ProductOnlineAvailability(Guid sourceId, bool isAvailable)
        {
            _priceSourceId = sourceId;
            _isAvailable = isAvailable;
        }

        public Guid PriceSourceId
        {
            get { return _priceSourceId; } 
            set { _priceSourceId = value; }
        }

        public bool IsAvailable
        {
            get { return _isAvailable; } 
            set { _isAvailable = value; }
        }
    }
}
