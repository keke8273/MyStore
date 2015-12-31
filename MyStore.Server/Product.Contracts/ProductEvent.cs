using CQRS.Infrastructure.EventSourcing;

namespace Product.Contracts
{
    public abstract class ProductEvent : VersionedEvent
    {
    }
}
