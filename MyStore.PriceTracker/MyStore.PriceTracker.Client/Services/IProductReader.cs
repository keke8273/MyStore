using System.Collections.Generic;

namespace MyStore.PriceTracker.Client.Services
{
    public interface IProductReader
    {
        IEnumerable<Product> GetProducts();
    }
}
