using System;

namespace Product.ReadModel
{
    public class ProductOnlineAvailibility
    {
        public Guid Id { get; set; }

        public bool IsAvailable { get; set; }

        public Guid ProductSourceId { get; set; }

        public Guid ProductId { get; set; }

        virtual public ProductSource ProductSource { get; set; }

        virtual public Product Product { get; set; }
    }
}
