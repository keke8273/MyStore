using System;

namespace Store.Dto
{
    public class ProductDto
    {
        public Guid BrandId { get; set; }
        public string BrandName { get; set; }
        public string Name { get; set; }
        public Uri ImageUrl { get; set; }
    }
}
