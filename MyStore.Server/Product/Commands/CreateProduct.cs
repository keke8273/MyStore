using CQRS.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Commands
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
