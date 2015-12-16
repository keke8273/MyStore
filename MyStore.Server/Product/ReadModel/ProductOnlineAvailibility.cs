using System;

namespace Product.ReadModel
{
    public class ProductOnlineAvailibility
    {
        public Guid Id { get; set; }

        public bool IsAvailable { get; set; }

        public Guid ProductSourceId { get; set; }

        public ProductSource ProductSource { get; set; }
    }
}
