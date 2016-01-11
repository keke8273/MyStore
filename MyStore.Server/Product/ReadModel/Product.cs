using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.ReadModel
{
    public class Product
    {
        public Product(Guid id, string brand, string name, string imageUrl)
            :this()
        {
            Id = id;
            Brand = brand;
            Name = name;
            ImageUrl = imageUrl;
        }

        protected Product()
        {
            Categories = new List<Category>();
        }

        public Guid Id { get; set; }
        [DataType(DataType.Url)]
        public string ImageUrl { get; set; }
        [Required]
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal LowestPrice { get; set; }
        public Guid LowestPriceSourceId { get; set; }
        public bool IsAvailableOnline { get; set; }
        public Guid AvailableOnlineSourceId { get; set; }

        #region Navigational Properties
        public ICollection<Category> Categories { get; set; } 
        #endregion
    }
}
