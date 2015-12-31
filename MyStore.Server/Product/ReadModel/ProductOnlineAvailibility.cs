using System;

namespace Store.ReadModel
{
    public class ProductOnlineAvailibility
    {
        public bool IsAvailable { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductSourceId { get; set; }
        virtual public ProductSource ProductSource { get; set; }
        virtual public Product Product { get; set; }
    }
}
