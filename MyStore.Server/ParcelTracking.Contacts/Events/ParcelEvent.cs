using System;
using CQRS.Infrastructure.EventSourcing;
using CQRS.Infrastructure.Messaging;
using CQRS.Infrastructure.Utils;

namespace ParcelTracking.Contacts.Events
{
    public abstract class ParcelEvent : VersionedEvent, ITimeStampedEvent
    {
        protected ParcelEvent()
        {
            TimeStamp = DateTimeUtil.Now;
        }

        public DateTime TimeStamp { get; private set; }
    }
}
