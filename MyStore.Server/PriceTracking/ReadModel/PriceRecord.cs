using System;
using CQRS.Infrastructure.Utils;

namespace ProductTracking.ReadModel
{
    public class PriceRecord
    {
        public PriceRecord()
        {
            Id = GuidUtil.NewSequentialId();
        }

        public Guid Id { get; set; }
        public Decimal Value { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductSourceId { get; set; }
    }
}