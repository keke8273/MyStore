using System;

namespace Store.ReadModel
{
    public interface IProductDao
    {
        Product GetProduct(Guid productId);

        Guid? LocateProduct(string name);
    }
}
