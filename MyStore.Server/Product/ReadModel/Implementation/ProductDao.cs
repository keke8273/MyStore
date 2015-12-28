using System;
using System.Data.Entity;
using System.Linq;
using CQRS.Infrastructure.Serialization;

namespace Product.ReadModel.Implementation
{
    public class ProductDao : IProductDao
    {
        private readonly Func<ProductDbContext> _contextFactory;
        private ITextSerializer _serializer;

        public ProductDao(Func<ProductDbContext> contextFactory, ITextSerializer serializer)
        {
            this._contextFactory = contextFactory;
            this._serializer = serializer;
        }

        public Product GetProduct(Guid productId)
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.Query<Product>().
                    Include(p => p.Brand).
                    Include(p =>p.Prices).
                    Include(p => p.OnlineAvailibilities).
                    Include(p => p.Stocks).
                    FirstOrDefault(dto => dto.ProductId == productId);
            }
        }

        public Guid? LocateProduct(string name)
        {
            using (var context = _contextFactory.Invoke())
            {
                var projectProjection = context
                    .Query<Product>()
                    .Where(p => p.Name == name)
                    .Select(m => new {m.ProductId})
                    .FirstOrDefault();

                if (projectProjection != null)
                {
                    return projectProjection.ProductId;
                }

                return null;
            }
        }
    }
}
