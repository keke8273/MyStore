using CQRS.Infrastructure.EventSourcing;

namespace Store.Contracts
{
    public abstract class ProductEvent : VersionedEvent
    {
    }
}
