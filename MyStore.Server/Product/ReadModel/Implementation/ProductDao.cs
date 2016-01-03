using System;
using System.Data.Entity;
using System.Linq;
using CQRS.Infrastructure.Serialization;
using CQRS.Infrastructure.Utils;

namespace Store.ReadModel.Implementation
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
                    FirstOrDefault(dto => dto.Id == productId);
            }
        }

        public Guid? LocateProduct(string name)
        {
            using (var context = _contextFactory.Invoke())
            {
                var product = context
                    .Query<Product>()
                    .Where(p => p.Name == name)
                    .Select(m => new {ProductId = m.Id})
                    .FirstOrDefault();

                if (product != null)
                {
                    return product.ProductId;
                }

                return null;
            }
        }
    }
}
