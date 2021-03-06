﻿using System;
using System.Web;
using System.Web.Http;
using CQRS.Infrastructure.Messaging;
using Microsoft.Practices.Unity;
using ParcelTracking.ReadModel;
using ParcelTracking.ReadModel.Implementation;
using Store.ReadModel;
using Store.ReadModel.Implementation;

namespace MyStore.Server.WebApi
{
    public partial class WebApiApplication : HttpApplication
    {
        private IUnityContainer _container;

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //Database intializer
            DatabaseSetup.Initialize();

            //Automapper initializer
            AutomapperConfiguration.Configure();

            _container = CreateContainer();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(_container);

            this.OnStart();
        }

        protected void Application_Stop()
        {
            this.OnStop();

            this._container.Dispose();
        }

        private static UnityContainer CreateContainer()
        {
            var container = new UnityContainer();
            try
            {
                container.RegisterType<ProductDbContext>(new TransientLifetimeManager(), new InjectionConstructor(ProductDbContext.SchemaName));
                container.RegisterType<ParcelStatusDbContext>(new TransientLifetimeManager(), new InjectionConstructor(ParcelStatusDbContext.SchemaName));

                container.RegisterType<IProductDao, ProductDao>();
                container.RegisterType<IParcelStatusDao, ParcelStatusDao>();

                OnCreateContainer(container);
            }
            catch (Exception)
            {
                container.Dispose();
                throw;
            }

            return container;
        }

        static partial void OnCreateContainer(UnityContainer container);

        partial void OnStart();

        partial void OnStop();
    }
}
