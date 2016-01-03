using System;
using System.Collections.Generic;

namespace Store.ReadModel
{
    public class OnlineAvailability
    {
        public bool IsAvailable { get; set; }
        public DateTime LastUpdated { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductSourceId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Source Source { get; set; }
        public virtual ICollection<OnlineAvailabilityRecord> OnlineAvailabilityHistory { get; set; }
    }

    public class OnlineAvailabilityRecord
    {
        public Guid Id { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductSourceId { get; set; }
        public virtual OnlineAvailability OnlineAvailability { get; set; }
    }
}
