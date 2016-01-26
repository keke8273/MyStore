using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ParcelTracking.Contacts;

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
            using (var context = _contextFactory.Invoke())
            {
                return context.Query<ParcelStatusRecord>()
                    .Where(dto => dto.ParcelStatusId == id)
                    .ToList();
            }
        }

        public IList<ParcelStatus> FindParcelByExpressProvider(string providerName)
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.Query<ParcelStatus>()
                    .Where(dto => dto.ExpressProvider.Name == providerName)
                    .ToList();
            }
        }

        public IList<ParcelStatus> FindUndeliveredParcels()
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.Query<ParcelStatus>()
                    .Where(dto => dto.StateValue != (int)ParcelState.Delivered)
                    .ToList();
            }
        }

        public ParcelStatus GetParcel(Guid id)
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.Query<ParcelStatus>().Include(p => p.ParcelStatusHistory).FirstOrDefault(dto => dto.Id == id);
            }
        }

        public ExpressProvider FindExpressProvider(Guid expressProviderId)
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.Query<ExpressProvider>().FirstOrDefault(dto => dto.Id == expressProviderId);
            }
        }
    }
}
