using CQRS.Infrastructure.EventSourcing;
using CQRS.Infrastructure.Messaging.Handling;
using CQRS.Infrastructure.Utils;
using ProductTracking.Contracts.Commands;

namespace ProductTracking.Handlers
{
    public class ProductPriceCommandHandler :
        ICommandHandler<UpdateProductPrice>
    {
        private readonly IEventSourcedRepository<ProductPrice> _repository;

        public ProductPriceCommandHandler(IEventSourcedRepository<ProductPrice> repository)
        {
            this._repository = repository;
        }

        public void Handle(UpdateProductPrice command)
        {
            var productPrice = _repository.Find(command.ProductId);

            if (productPrice == null)
                productPrice = new ProductPrice(command.ProductId);

            productPrice.UpdatePrice(command.ProductSourceId, command.Price);

            _repository.Save(productPrice, command.Id.ToString());
        }
    }
}
