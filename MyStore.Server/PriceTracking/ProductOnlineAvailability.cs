﻿using System;
using System.Collections.Generic;
using CQRS.Infrastructure.EventSourcing;
using MyStore.Common;
using ProductTracking.Contracts.Events;
using CQRS.Infrastructure.Utils;

namespace ProductTracking
{
    public class ProductOnlineAvailability : EventSourced
    {
        private readonly Dictionary<Guid, bool> _availabilities;

        public ProductOnlineAvailability(Guid id) : base(id)
        {
            RegisterHandler<OnlineAvailabilityUpdated>(OnOnlineAvailabilityUpdated);
            _availabilities = new Dictionary<Guid, bool>();
        }

        public ProductOnlineAvailability(Guid id, IEnumerable<IVersionedEvent> history)
            : this(id)
        {
            LoadFrom(history);
        }

        public void UpdateOnlineAvailability(Guid productSourceId, bool isAvailable)
        {
            bool currentAvailability;

            if (_availabilities.TryGetValue(productSourceId, out currentAvailability))
            {
                _availabilities[productSourceId] = isAvailable;
            }
            else
            {
                currentAvailability = false;
                _availabilities.Add(productSourceId, isAvailable);
            }

            Update(new OnlineAvailabilityUpdated(DateTimeUtil.Now)
            {
                ProductSourceId = productSourceId,
                NewAvailability = isAvailable,
                PreviousAvailability = currentAvailability
            });
        }

        private void OnOnlineAvailabilityUpdated(OnlineAvailabilityUpdated @event)
        {
            bool currentAvailability;

            if (_availabilities.TryGetValue(@event.ProductSourceId, out currentAvailability))
            {
                _availabilities[@event.ProductSourceId] = @event.NewAvailability;
            }
            else
            {
                currentAvailability = false;
                _availabilities.Add(@event.ProductSourceId, @event.NewAvailability);
            }
        }
    }
}
