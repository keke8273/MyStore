using System.Data.Entity;
using CQRS.Infrastructure.Messaging;
using CQRS.Infrastructure.Serialization;
using CQRS.Infrastructure.Sql.Messaging;
using Microsoft.Practices.Unity;

namespace MyStore.Server.WebApi
{
    public partial class WebApiApplication
    {
        static partial void OnCreateContainer(UnityContainer container)
        {
            var serializer = new JsonTextSerializer();
            container.RegisterInstance<ITextSerializer>(serializer);

            container.RegisterType<IMessageSender, MessageSender>("Commands", new TransientLifetimeManager(),
                new InjectionConstructor(Database.DefaultConnectionFactory, "SqlBus", "SqlBus.Command"));
            container.RegisterType<ICommandBus, CommandBus>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IMessageSender>("Commands"), serializer));

            container.RegisterType<IMessageSender, MessageSender>("Events", new TransientLifetimeManager(),
                new InjectionConstructor(Database.DefaultConnectionFactory, "SqlBus", "SqlBus.Events"));
            container.RegisterType<IEventBus, EventBus>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IMessageSender>("Events"), serializer));
        }
    }
}