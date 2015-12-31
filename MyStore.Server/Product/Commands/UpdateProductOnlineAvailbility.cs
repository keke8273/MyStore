using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS.Infrastructure.Messaging;

namespace Product.Commands
{
    public class UpdateProductOnlineAvailibility : ICommand
    {
        public UpdateProductOnlineAvailibility()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public Guid ProductId { get; set; }
        public Guid ProductSourceId { get; set; }
        public bool IsAvailalbe { get; set; }
    }
}
