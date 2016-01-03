using System;
using System.Collections.Generic;

namespace Store.Dto
{
    public class ProductDto
    {
        public string Brand { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ProductDto) obj);
        }

        protected bool Equals(ProductDto other)
        {
            return string.Equals(Brand, other.Brand) && Equals(Categories, other.Categories) &&
                   string.Equals(Name, other.Name) && string.Equals(ImageUrl, other.ImageUrl);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Brand != null ? Brand.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Categories != null ? Categories.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (ImageUrl != null ? ImageUrl.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}