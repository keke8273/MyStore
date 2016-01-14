using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelTracking.ReadModel.Implementation
{
    public class ParcelStatusDao : IParcelStatusDao
    {
        private readonly Func<ParcelTrackingDbContext> _contextFactory;

        public ParcelStatusDao(Func<ParcelTrackingDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IList<ParcelRecord> GetParcelHistory(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<Parcel> FindParcelByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public IList<Parcel> FindParcelByExpressProvider(string providerName)
        {
            throw new NotImplementedException();
        }

        public Parcel GetParcel(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
