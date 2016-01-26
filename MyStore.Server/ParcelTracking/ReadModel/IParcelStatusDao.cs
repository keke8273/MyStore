using System;
using System.Collections.Generic;

namespace ParcelTracking.ReadModel
{
    public interface IParcelStatusDao
    {
        IList<ParcelStatusRecord> GetParcelHistory(Guid id);

        IList<ParcelStatus> FindParcelByExpressProvider(string providerName);

        IList<ParcelStatus> FindUndeliveredParcels();

        ParcelStatus GetParcel(Guid id);

        ExpressProvider FindExpressProvider(Guid expressProviderId);
    }
}
