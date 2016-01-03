using System.Diagnostics;
using CQRS.Infrastructure.EventSourcing;
using CQRS.Infrastructure.Messaging.Handling;
using MyStore.Common;
using Store.Commands;

namespace Store.Handlers
{
    public class ProductCommandHandler:
        ICommandHandler<UpdateProductPrice>,
        ICommandHandler<UpdateProductOnlineAvailibility>
    {
        private readonly IEventSourcedRepository<Product> _repository;
        private readonly IDateTimeService _dateTimeService;

        public ProductCommandHandler(IEventSourcedRepository<Product> repository, IDateTimeService dateTimeService)
        {
            this._repository = repository;
            _dateTimeService = dateTimeService;
        }

        public void Handle(UpdateProductPrice command)
        {
            var product = _repository.Find(command.ProductId);

            if (product == null)
            {
                Trace.TraceWarning("Product {0}, {1} does not exist.");
            }
            else
            {
                product.UpdatePrice(command.ProductSourceId, command.Price, _dateTimeService);
            }

            _repository.Save(product, command.Id.ToString());
        }

        public void Handle(UpdateProductOnlineAvailibility command)
        {

            var product = _repository.Find(command.ProductId);

            if (product == null)
            {
                Trace.TraceWarning("Product {0}, {1} does not exist.");
            }
            else
            {
                product.UpdateOnlineAvailibility(command.ProductSourceId, command.IsAvailalbe, _dateTimeService);
            }

            _repository.Save(product, command.Id.ToString());
        }
    }
}
