using System;

namespace ProductTracking.ReadModel
{
    public class OnlineAvailabilityRecord
    {
        public Guid Id { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductSourceId { get; set; }
    }
}