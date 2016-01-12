using System;
using CQRS.Infrastructure.Messaging;

namespace ProductTracking.Contracts.Commands
{
    public class UpdateProductOnlineAvailability : ICommand
    {
        public UpdateProductOnlineAvailability()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public Guid ProductId { get; set; }
        public Guid ProductSourceId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
