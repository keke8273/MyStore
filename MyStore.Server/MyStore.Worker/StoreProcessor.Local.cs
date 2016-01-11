using CQRS.Infrastructure;
using CQRS.Infrastructure.EventSourcing;
using CQRS.Infrastructure.Messaging;
using CQRS.Infrastructure.Messaging.Handling;
using CQRS.Infrastructure.Misc;
using CQRS.Infrastructure.Serialization;
using CQRS.Infrastructure.Sql.EventSourcing;
using CQRS.Infrastructure.Sql.MessageLog;
using CQRS.Infrastructure.Sql.Messaging;
using CQRS.Infrastructure.Sql.Messaging.Handling;
using CQRS.Infrastructure.Sql.Messaging.Implementation;
using Microsoft.Practices.Unity;
using System.Data.Entity;
using ProductTracking.Handlers;

namespace MyStore.Worker
{
    partial class StoreProcessor
    {
        partial void OnCreateContainer(UnityContainer container)
        {
            var serializer = container.Resolve<ITextSerializer>();
            var metadata = container.Resolve<IMetadataProvider>();

            var commandBus = new CommandBus(new MessageSender(Database.DefaultConnectionFactory, "MyStore", "SqlBus.Commands"), serializer);
            var eventBus = new EventBus(new MessageSender(Database.DefaultConnectionFactory, "MyStore", "SqlBus.Events"), serializer);

            var commandProcessor = new CommandProcessor(new MessageReceiver(Database.DefaultConnectionFactory, "MyStore", "SqlBus.Commands"), serializer);
            var eventProcessor = new EventProcessor(new MessageReceiver(Database.DefaultConnectionFactory, "MyStore", "SqlBus.Events"), serializer);

            container.RegisterInstance<ICommandBus>(commandBus);
            container.RegisterInstance<IEventBus>(eventBus);
            container.RegisterInstance<ICommandHandlerRegistry>(commandProcessor);
            container.RegisterInstance<IProcessor>("CommandProcessor", commandProcessor);
            container.RegisterInstance<IEventHandlerRegistery>(eventProcessor);
            container.RegisterInstance<IProcessor>("EventProcessor", eventProcessor);

            // Event logger
            container.RegisterType<SqlMessageLog>(new InjectionConstructor("MessageLog", serializer, metadata));
            container.RegisterType<IEventHandler, SqlMessageLogHandler>("SqlMessageLogHandler");
            container.RegisterType<ICommandHandler, SqlMessageLogHandler>("SqlMessageLogHandler");

            RegisterRepository(container);
            RegisterEventHandlers(container, eventProcessor);
            RegisterCommandHandlers(container);
        }
        private void RegisterRepository(UnityContainer container)
        {
            // repositories
            container.RegisterType<EventStoreDbContext>(new TransientLifetimeManager(),
                new InjectionConstructor("EventStore"));
            container.RegisterType(typeof (IEventSourcedRepository<>), typeof (SqlEventSourcedRepository<>),
                new ContainerControlledLifetimeManager());
        }

        private void RegisterEventHandlers(UnityContainer container, EventProcessor eventProcessor)
        {
            eventProcessor.Register(container.Resolve<ProductStatusViewModelGenerator>());
        }

        private void RegisterCommandHandlers(UnityContainer container)
        {
            var commandHandlerRegistry = container.Resolve<ICommandHandlerRegistry>();

            foreach (var commandHandler in container.ResolveAll<ICommandHandler>())
            {
                commandHandlerRegistry.Register(commandHandler);
            }
        }
    }
}
