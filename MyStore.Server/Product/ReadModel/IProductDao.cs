using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ReadModel
{
    public interface IProductDao
    {
        Product GetProduct(Guid productId);

        Guid? LocateProduct(string name);
    }
}
