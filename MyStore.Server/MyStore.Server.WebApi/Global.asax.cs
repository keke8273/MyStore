﻿using System;
using System.Data.SqlTypes;
using System.Web;
using System.Web.Http;
using Microsoft.Practices.Unity;
using Product.ReadModel;
using Product.ReadModel.Implementation;

namespace MyStore.Server.WebApi
{
    public partial class WebApiApplication : HttpApplication
    {
        private IUnityContainer _container;

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            DatabaseSetup.Initialize();

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
                container.RegisterType<ProductDbContext>(new TransientLifetimeManager(), new InjectionConstructor("StoreManagement"));

                container.RegisterType<IProductDao, ProductDao>();

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
