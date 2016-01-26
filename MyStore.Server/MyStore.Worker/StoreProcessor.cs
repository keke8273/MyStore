using System.Runtime.Serialization;
using CQRS.Infrastructure;
using CQRS.Infrastructure.Database;
using CQRS.Infrastructure.Messaging;
using CQRS.Infrastructure.Messaging.Handling;
using CQRS.Infrastructure.Misc;
using CQRS.Infrastructure.Processes;
using CQRS.Infrastructure.Serialization;
using CQRS.Infrastructure.Sql.Database;
using CQRS.Infrastructure.Sql.Processes;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using ParcelTracking;
using ParcelTracking.Database;
using ParcelTracking.Handlers;
using ParcelTracking.Interpreters;
using ParcelTracking.ReadModel;
using ParcelTracking.ReadModel.Implementation;
using ParcelTracking.Trackers;
using ProductTracking.Handlers;
using Store.ReadModel;
using Store.ReadModel.Implementation;
using CQRS.Infrastructure.Utils;

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

            //Utility classes
            container.RegisterInstance<ITextSerializer>(new JsonTextSerializer());
            container.RegisterInstance<IMetadataProvider>(new StandardMetaDataProvider());

            //DbContexts
            container.RegisterType<DbContext, ParcelDbContext>("parcel", new TransientLifetimeManager(),
                new InjectionConstructor(ParcelDbContext.SchemaName));
            container.RegisterType<IDataContext<Parcel>, SqlDataContext<Parcel>>(new TransientLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<Func<DbContext>>("parcel"), typeof(IEventBus)));

            //container.RegisterType<DbContext, ParcelTrackingProcessManagerDbContext>("parcelTracking", new TransientLifetimeManager(), new InjectionConstructor("ConferenceRegistrationProcesses"));

            //container.RegisterType<IProcessManagerDataContext<ParcelTrackingProcessManager>, SqlProcessManagerDataContext<ParcelTrackingProcessManager>>(
            //    new TransientLifetimeManager(),
            //    new InjectionConstructor(new ResolvedParameter<Func<DbContext>>("parcelTracking"), typeof(ICommandBus), typeof(ITextSerializer)));

            container.RegisterType<DbContext, ProductDbContext>(new TransientLifetimeManager(), new InjectionConstructor(ProductDbContext.SchemaName));
            container.RegisterType<DbContext, ParcelStatusDbContext>(new TransientLifetimeManager(), new InjectionConstructor(ParcelStatusDbContext.SchemaName));

            //Daos
            container.RegisterType<IProductDao, ProductDao>(new ContainerControlledLifetimeManager());
            container.RegisterType<IParcelStatusDao, ParcelStatusDao>(new ContainerControlledLifetimeManager());

            //Handlers
            container.RegisterType<ICommandHandler, ProductPriceCommandHandler>("ProductPriceCommandHandler");
            container.RegisterType<ICommandHandler, OnlineAvailabilityCommandHandler>("OnlineAvailabilityCommandHandler");
            container.RegisterType<ICommandHandler, ParcelCommandHandler>("ParcelCommandHandler");

            //Services
            container.RegisterType<IDateTimeService, DateTimeService>(new ContainerControlledLifetimeManager());
            var trackingService = new TrackingService(container.Resolve<IParcelStatusDao>());
            container.RegisterInstance<ITrackingService>(trackingService);
            var interpretingService = new InterpretingService(container.Resolve<IParcelStatusDao>());
            container.RegisterInstance<IInterpretingService>(interpretingService);

            //Processors
            container.RegisterType<IProcessor, ParcelTrackingProcessor>("ParcelTrackingProcessor", new ContainerControlledLifetimeManager());

            OnCreateContainer(container);

            DateTimeUtil.RegisterDateTimeService(container.Resolve<IDateTimeService>());
            RegisterTrackers(container, trackingService);
            RegisterInterpreters(container, interpretingService);

            return container;
        }

        private void RegisterInterpreters(UnityContainer container, InterpretingService interpretingService)
        {
            interpretingService.RegisterInterpreter(container.Resolve<EmmsInterpreter>());
        }

        private void RegisterTrackers(UnityContainer container, TrackingService trackingService)
        {
            trackingService.RegisterTrackers(container.Resolve<EmmsTracker>());
        }

        partial void OnCreateContainer(UnityContainer container);

    }
}
