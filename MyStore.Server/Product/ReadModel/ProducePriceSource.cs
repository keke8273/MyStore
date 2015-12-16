using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ReadModel
{
    public class PriceSource
    {
        public Guid PriceSourceId { get; set; }

        [Required]
        public string Name { get; set; }

        public Uri LogoUri { get; set; }
    }
}
