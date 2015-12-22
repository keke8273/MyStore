using CQRS.Infrastructure;
using CQRS.Infrastructure.Messaging;
using CQRS.Infrastructure.Messaging.Handling;
using CQRS.Infrastructure.Misc;
using CQRS.Infrastructure.Serialization;
using CQRS.Infrastructure.Sql.Messaging;
using CQRS.Infrastructure.Sql.Messaging.Handling;
using CQRS.Infrastructure.Sql.Messaging.Implementation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Worker
{
    partial class StoreProcessor
    {
        partial void OnCreateContainer(UnityContainer container)
        {
            var serializer = container.Resolve<ITextSerializer>();
            var metadata = container.Resolve<IMetadataProvider>();

            var commandBus = new CommandBus(new MessageSender(Database.DefaultConnectionFactory, "SqlBus", "SqlBus.Commands"), serializer);
            var eventBus = new EventBus(new MessageSender(Database.DefaultConnectionFactory, "SqlBus", "SqlBus.Events"), serializer);

            var commandProcessor = new CommandProcessor(new MessageReceiver(Database.DefaultConnectionFactory, "SqlBus", "SqlBus.Commands"), serializer);
            var eventProcessor = new EventProcessor(new MessageReceiver(Database.DefaultConnectionFactory, "SqlBus", "SqlBus.Events"), serializer);

            container.RegisterInstance<ICommandBus>(commandBus);
            container.RegisterInstance<IEventBus>(eventBus);
            container.RegisterInstance<ICommandHandlerRegistry>(commandProcessor);
            container.RegisterInstance<IProcessor>("CommandProcessor", commandProcessor);
            container.RegisterInstance<IEventHandlerRegistery>(eventProcessor);
            container.RegisterInstance<IProcessor>("EventProcessor", eventProcessor);

            container.RegisterType<SqlMessageLog>(new InjectionConstructor("MessageLog", serializer, metadata));
            container.RegisterType();
        }
    }
}
