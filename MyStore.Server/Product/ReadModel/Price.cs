using System;
using System.Collections.Generic;

namespace Store.ReadModel
{
    public class Price
    {
        public decimal Value { get; set; }
        public DateTime LastUpdated { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductSourceId { get; set; }
        #region Navitgation
		public virtual Product Product {get; set;}
        public virtual Source Source { get; set; }
        public virtual ICollection<PriceRecord> PriceHistory { get; set; }
        #endregion
    }

    public class PriceRecord
    {
        public Guid Id { get; set; }
        public Decimal Value { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductSourceId { get; set; }
        public virtual Price Price { get; set; }
    }
}
