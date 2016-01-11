using System;
using System.Collections.Generic;

namespace ProductTracking.ReadModel
{
    public interface IProductStatusDao
    {
        IList<PriceRecord> GetPriceHistory(Guid productId, Guid productSourceId);
        IList<OnlineAvailabilityRecord> GetOnlineAvailabilityHistory(Guid productId, Guid productSourceId);
    }
}
