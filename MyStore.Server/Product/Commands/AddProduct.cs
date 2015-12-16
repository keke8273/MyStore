using CQRS.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Commands
{
    public class AddProduct : ICommand
    {
        protected AddProduct()
        {
            Id = Guid.NewGuid();
        }

        public AddProduct()
            :this()
        {

        }

        public Guid Id{get; set;}
    }
}
