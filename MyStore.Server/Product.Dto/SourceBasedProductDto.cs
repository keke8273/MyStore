using System;

namespace Store.Dto
{
    public class SourceBasedProductDto : ProductDto
    {
        public decimal RetailPrice { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailableOnline { get; set; }
        public string SourceName { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SourceBasedProductDto) obj);
        }

        protected bool Equals(SourceBasedProductDto other)
        {
            return base.Equals(other) && RetailPrice == other.RetailPrice && Price == other.Price &&
                   IsAvailableOnline.Equals(other.IsAvailableOnline) &&
                   string.Equals(SourceName, other.SourceName);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ RetailPrice.GetHashCode();
                hashCode = (hashCode * 397) ^ Price.GetHashCode();
                hashCode = (hashCode * 397) ^ IsAvailableOnline.GetHashCode();
                hashCode = (hashCode * 397) ^ (SourceName != null ? SourceName.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}