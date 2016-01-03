using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            Prices = new List<Price>();
            Stocks = new List<Stock>();
            OnlineAvailibilities = new List<OnlineAvailability>();
            Categories = new List<Category>();
        }

        public Guid Id { get; set; }

        [DataType(DataType.Url)]
        public string ImageUrl { get; set; }

        [Required]
        public string Name { get; set; }

        public string Brand { get; set; }

        #region Navigational Properties
        public ICollection<Category> Categories { get; set; } 

        public virtual ICollection<Price> Prices { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }

        public virtual ICollection<OnlineAvailability> OnlineAvailibilities { get; set; }

        #endregion
    }
}
