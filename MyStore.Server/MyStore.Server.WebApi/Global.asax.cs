using System.Web;
using System.Web.Http;
using Microsoft.Practices.Unity;

namespace MyStore.Server.WebApi
{
    public partial class WebApiApplication : HttpApplication
    {
        private IUnityContainer container;

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            container = CreateContainer();
        }

        private static UnityContainer CreateContainer()
        {
            throw new System.NotImplementedException();
        }
    }
}
