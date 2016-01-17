using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelTracking.ReadModel.Implementation
{
    public class ParcelStatusDao : IParcelStatusDao
    {
        private readonly Func<ParcelStatusDbContext> _contextFactory;

        public ParcelStatusDao(Func<ParcelStatusDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IList<ParcelStatusRecord> GetParcelHistory(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<ParcelStatus> FindParcelByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public IList<ParcelStatus> FindParcelByExpressProvider(string providerName)
        {
            throw new NotImplementedException();
        }

        public ParcelStatus GetParcel(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
