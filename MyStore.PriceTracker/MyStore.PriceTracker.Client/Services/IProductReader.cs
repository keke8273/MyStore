using System.Collections.Generic;
using Store.Dto;

namespace MyStore.PriceTracker.Client.Services
{
    public interface IProductReader
    {
        IEnumerable<SourceBasedProductDto> GetProductInfo();
    }
}
