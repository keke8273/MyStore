using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Contracts
{
    public class ProductCreated : ProductEvent
    {
        public string Name { get; set; }

        public Guid BrandId { get; set; }

        public Uri ImageUri { get; set; }
    }
}
