using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductTracking.ReadModel.Implementation
{
    public class ProductStatusDao : IProductStatusDao
    {
        private readonly Func<ProductStatusDbContext> _contextFactory;

        public ProductStatusDao(Func<ProductStatusDbContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }

        public IList<PriceRecord> GetPriceHistory(Guid productId, Guid productSourceId)
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.Query<PriceRecord>()
                    .Where(pr => pr.ProductId == productId && pr.ProductSourceId == productSourceId)
                    .ToList();
            }
        }

        public IList<OnlineAvailabilityRecord> GetOnlineAvailabilityHistory(Guid productId, Guid productSourceId)
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.Query<OnlineAvailabilityRecord>()
                    .Where(oa => oa.ProductId == productId && oa.ProductSourceId == productSourceId)
                    .ToList();
            }
        }
    }
}
