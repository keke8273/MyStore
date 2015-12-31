using CQRS.Infrastructure;
using CQRS.Infrastructure.Messaging.Handling;
using CQRS.Infrastructure.Misc;
using CQRS.Infrastructure.Serialization;
using Microsoft.Practices.Unity;
using MyStore.Common;
using MyStore.Common.Implementation;
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
        private readonly IUnityContainer _container;
        private CancellationTokenSource cancellationTokenSource;
        private List<IProcessor> processors;
        private bool instrumentationEnabled;

        public StoreProcessor(bool instrumentationEnabled = false)
        {
            this.instrumentationEnabled = instrumentationEnabled;

            //OnCreating();

            this.cancellationTokenSource = new CancellationTokenSource();
            this._container = CreateContainer();

            this.processors = _container.ResolveAll<IProcessor>().ToList();
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
            this._container.Dispose();
            this.cancellationTokenSource.Dispose();
        }

        private UnityContainer CreateContainer()
        {
            var container = new UnityContainer();

            //Services
            container.RegisterType<IDateTimeService, DateTimeService>(new ContainerControlledLifetimeManager());

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
