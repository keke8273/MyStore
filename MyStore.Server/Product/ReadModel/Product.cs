using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Product.ReadModel
{
    public class Product
    {
        public Product(Guid productId, Guid brandId)
        { }

        protected Product()
        {
            Prices = new List<ProductPrice>();
            Stocks = new List<ProductStock>();
            OnlineAvailibilities = new List<ProductOnlineAvailibility>();
        }

        public Guid ProductId { get; set; }

        public Guid BrandId { get; set; }
        
        [Required]
        public string Name { get; set; }

        [DataType(DataType.Url)]
        public string ImageUri { get; set; }

        public virtual Brand Brand { get; set; }

        public ICollection<ProductPrice> Prices { get; set; }

        public ICollection<ProductStock> Stocks { get; set; }

        public ICollection<ProductOnlineAvailibility> OnlineAvailibilities { get; set; } 
    }
}
