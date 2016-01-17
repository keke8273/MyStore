using System;
using CQRS.Infrastructure.Messaging.Handling;
using CQRS.Infrastructure.Processes;
using Subscription.Contracts;

namespace ParcelTracking
{
    public class ParcelTrackingProcessManagerRouter :
        IEventHandler<ParcelSubscriptionCreated>
    {
        private readonly Func<IProcessManagerDataContext<ParcelTrackingProcessManager>> _contextFactory;

        public ParcelTrackingProcessManagerRouter(Func<IProcessManagerDataContext<ParcelTrackingProcessManager>> contextFactory)
        {
            this._contextFactory = contextFactory;
        }

        public void Handle(ParcelSubscriptionCreated @event)
        {
            using (var context = _contextFactory.Invoke())
            {
                var pm = context.Find(x => x.ParcelId == @event.SourceId);

                if (pm == null)
                {
                    pm = new ParcelTrackingProcessManager();
                }

                pm.Handle(@event);

                context.Save(pm);
            }
        }
    }
}
