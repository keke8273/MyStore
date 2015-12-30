using System;
using System.Collections.Generic;

namespace Product.ReadModel
{
    public interface IBrandDao
    {
        Guid CreateBrand(Brand brand);
        IEnumerable<Brand> GetAllBrands();
    }
}