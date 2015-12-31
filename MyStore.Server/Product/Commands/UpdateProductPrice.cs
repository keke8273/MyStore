using System;
using CQRS.Infrastructure.Messaging;

namespace Product.Commands
{
    public class UpdateProductPrice : ICommand
    {
        public UpdateProductPrice()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public Guid ProductId { get; set; }
        public Guid ProductSourceId { get; set; }
        public decimal Price { get; set; }
    }
}
