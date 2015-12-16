using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ReadModel
{
    public class Brand
    {
        public Guid BrandId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}
