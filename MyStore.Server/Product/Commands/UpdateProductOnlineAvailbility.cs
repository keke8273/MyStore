using System;
using CQRS.Infrastructure.Messaging;

namespace Store.Commands
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
