using System.Diagnostics;
using CQRS.Infrastructure.EventSourcing;
using CQRS.Infrastructure.Messaging.Handling;
using Product.Commands;

namespace Product.Handlers
{
    public class ProductCommandHandler:
        ICommandHandler<CreateProduct>
    {
        private readonly IEventSourcedRepository<Product> _repository;

        public ProductCommandHandler(IEventSourcedRepository<Product> repository)
        {
            this._repository = repository;
        }

        public void Handle(CreateProduct command)
        {
            var product = _repository.Find(command.ProductId);

            if (product == null)
            {
                product = new Product(command.ProductId, command.BrandId, command.ProductName, command.ImageUrl);
            }
            else
            {
                Trace.TraceWarning("Product {0}, {1} was already created", command.ProductId, command.ProductName);
            }

            _repository.Save(product, command.Id.ToString());
        }
    }
}
