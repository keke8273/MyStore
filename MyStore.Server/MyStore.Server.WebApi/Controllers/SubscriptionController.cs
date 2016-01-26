using System;
using System.Web.Http;
using System.Web.Http.Description;
using CQRS.Infrastructure.Messaging;
using CQRS.Infrastructure.Utils;
using Subscription;
using Subscription.Dto;

namespace MyStore.Server.WebApi.Controllers
{
    [RoutePrefix("api/subscription")]
    public class SubscriptionController : ApiController
    {
        private readonly IEventBus _eventBus;

        private SubscriptionServcie _subscriptionService;

        private SubscriptionServcie Service {
            get { return _subscriptionService ?? (_subscriptionService = new SubscriptionServcie(_eventBus)); }
        }

        public SubscriptionController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpPost]
        [ResponseType(typeof(Guid))]
        public IHttpActionResult CreateParcelTrackingSubscription(SubscriptionDto subscriptionDto)
        {
            try
            {
                Service.CreateParcelSubscription(subscriptionDto.SubscribeeId, subscriptionDto.SubscriberId);

                return Ok(subscriptionDto.SubscribeeId);
            }
            catch (DuplicateSubscriptionException ex)  
            {
                return BadRequest(String.Format("parcel {0} is already subscribed.", subscriptionDto.SubscribeeId));
            }
        }
    }
}