using System;
using System.Collections.Generic;

namespace Store.ReadModel
{
    public interface IBrandDao
    {
        Guid CreateBrand(Brand brand);
        IEnumerable<Brand> GetAllBrands();
    }
}