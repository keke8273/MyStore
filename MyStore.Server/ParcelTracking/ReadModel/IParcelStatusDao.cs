using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelTracking.ReadModel
{
    public interface IParcelStatusDao
    {
        IList<ParcelStatusRecord> GetParcelHistory(Guid id);

        IList<ParcelStatus> FindParcelByUser(Guid userId);

        IList<ParcelStatus> FindParcelByExpressProvider(string providerName);

        ParcelStatus GetParcel(Guid id);
    }
}
