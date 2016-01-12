using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelTracking.ReadModel
{
    public interface IParcelStatusDao
    {
        IList<ParcelRecord> GetParcelHistory(Guid id);

        IList<Parcel> FindParcelByUser(Guid userId);

        IList<Parcel> FindParcelByExpressProvider(string providerName);

        Parcel GetParcel(Guid id);
    }
}
