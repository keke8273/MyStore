using System;
using CQRS.Infrastructure.Messaging;

namespace Store.Commands
{
    public class CreateProduct : ICommand
    {
        public CreateProduct()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id{get; private set;}

        public Guid ProductId { get; set; }

        public Guid BrandId { get; set; }

        public string ProductName { get; set; }

        public Uri ImageUrl { get; set; }  
    }
}
