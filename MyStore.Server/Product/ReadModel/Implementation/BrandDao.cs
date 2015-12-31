using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.ReadModel.Implementation
{
    public class BrandDao : IBrandDao
    {
        private readonly Func<ProductDbContext> _contextFactory;

        public BrandDao(Func<ProductDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public Guid CreateBrand(Brand brand)
        {
            using (var context = _contextFactory.Invoke())
            {
                context.Set<Brand>().Add(brand);
                context.SaveChanges();
            }

            return brand.Id;
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.Set<Brand>().ToList();
            }
        }
    }
}
