using CQRS.Infrastructure.Messaging.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductPriceTracking.Events;

namespace Store.Handlers
{
    public class ProductViewModelGenerator :
        IEventHandler<ProductPriceUpdated>,
        IEventHandler<OnlineAvailabilityUpdated>
    {

    }
}
