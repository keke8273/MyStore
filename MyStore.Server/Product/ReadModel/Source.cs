using System;
using System.ComponentModel.DataAnnotations;

namespace Store.ReadModel
{
    public class Source
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Url)]
        public string LogoUrl { get; set; }
    }
}
