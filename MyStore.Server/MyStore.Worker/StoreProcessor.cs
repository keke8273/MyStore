using CQRS.Infrastructure;
using CQRS.Infrastructure.Messaging.Handling;
using CQRS.Infrastructure.Misc;
using CQRS.Infrastructure.Serialization;
using Microsoft.Practices.Unity;
using Product.Handlers;
using Product.ReadModel;
using Product.ReadModel.Implementation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyStore.Worker
{
    public sealed partial class StoreProcessor : IDisposable
    {
        private IUnityContainer container;
        private CancellationTokenSource cancellationTokenSource;
        private List<IProcessor> processors;
        private bool instrumentationEnabled;

        public StoreProcessor(bool instrumentationEnabled = false)
        {
            this.instrumentationEnabled = instrumentationEnabled;

            //OnCreating();

            this.cancellationTokenSource = new CancellationTokenSource();
            this.container = CreateContainer();

            this.processors = container.ResolveAll<IProcessor>().ToList();
        }

        public void Start()
        {
            processors.ForEach(p => p.Start());
        }

        public void Stop()
        {
            cancellationTokenSource.Cancel();

            processors.ForEach(p => p.Stop());
        }

        public void Dispose()
        {
            this.container.Dispose();
            this.cancellationTokenSource.Dispose();
        }

        private UnityContainer CreateContainer()
        {
            var container = new UnityContainer();

            //Utility classes
            container.RegisterInstance<ITextSerializer>(new JsonTextSerializer());
            container.RegisterInstance<IMetadataProvider>(new StandardMetaDataProvider());

            //DbContexts
            container.RegisterType<DbContext, ProductDbContext>("product", new TransientLifetimeManager(), new InjectionConstructor(ProductDbContext.SchemaName));

            //Daos
            container.RegisterType<IProductDao, ProductDao>(new ContainerControlledLifetimeManager());

            //handlers
            container.RegisterType<ICommandHandler, ProductCommandHandler>("ProductCommandHandler");

            OnCreateContainer(container);

            return container;
        }

        partial void OnCreateContainer(UnityContainer container);
    }
}
