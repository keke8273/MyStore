using System.Diagnostics;
using CQRS.Infrastructure.EventSourcing;
using CQRS.Infrastructure.Messaging.Handling;
using MyStore.Common;
using ProductTracking.Commands;

namespace ProductTracking.Handlers
{
    public class ProductPriceCommandHandler :
        ICommandHandler<UpdateProductPrice>
    {
        private readonly IEventSourcedRepository<ProductPrice> _repository;
        private readonly IDateTimeService _dateTimeService;

        public ProductPriceCommandHandler(IEventSourcedRepository<ProductPrice> repository, IDateTimeService dateTimeService)
        {
            this._repository = repository;
            _dateTimeService = dateTimeService;
        }

        public void Handle(UpdateProductPrice command)
        {
            var productPrice = _repository.Find(command.ProductId);

            if (productPrice == null)
                productPrice = new ProductPrice(command.ProductId);

            productPrice.UpdatePrice(command.ProductSourceId, command.Price, _dateTimeService);

            _repository.Save(productPrice, command.Id.ToString());
        }
    }
}
