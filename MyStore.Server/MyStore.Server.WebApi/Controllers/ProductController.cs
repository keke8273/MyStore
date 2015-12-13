using System.Web.Http;
using Product.ReadModel;

namespace MyStore.Server.WebApi.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductDao productDao;

        public ProductController(IProductDao productDao)
        {
            productDao = productDao;
        }



    }
}
