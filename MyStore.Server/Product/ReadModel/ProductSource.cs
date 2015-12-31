using System;
using System.ComponentModel.DataAnnotations;

namespace Store.ReadModel
{
    public class ProductSource
    {
        public Guid ProductSourceId { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Url)]
        public string LogoUrl { get; set; }
    }
}
