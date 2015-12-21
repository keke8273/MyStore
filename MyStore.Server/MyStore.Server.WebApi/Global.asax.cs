using System.Web;
using System.Web.Http;
using Microsoft.Practices.Unity;

namespace MyStore.Server.WebApi
{
    public class WebApiApplication : HttpApplication
    {
        private IUnityContainer container;

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            container = CreateContainer();

            this.OnStart();
        }

        protected void Application_Stop()
        {
            this.OnStop();

            this.container.Dispose();
        }

        private static UnityContainer CreateContainer()
        {
            throw new System.NotImplementedException();
        }

        private void OnStart()
        {
        }

        private void OnStop()
        {
        }
    }
}
