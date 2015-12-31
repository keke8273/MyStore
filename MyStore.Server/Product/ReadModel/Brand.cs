using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.ReadModel
{
    public class Brand
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}
