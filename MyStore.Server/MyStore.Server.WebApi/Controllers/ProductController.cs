using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Windows.Input;
using Product.ReadModel;

namespace MyStore.Server.WebApi.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductDao productDao;
        private readonly ICommand _bus;

        public ProductController(IProductDao productDao, ICommand bus)
        {
            _bus = bus;
            productDao = productDao;
        }

        [ResponseType(typeof (Product))]
        public async Task<IHttpActionResult> GetProduct(Guid productId)
        {
            var product = productDao.FindProduct(productId);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        public async Task<IHttpActionResult> LocateProductId(string name)
        {
            var productId = productDao.LocateProduct(name);

            if (!productId.HasValue)
            {
                return NotFound();
            }

            return Ok(productId.Value);
        }

        public async Task<IHttpActionResult> CreateProduct(string name, Guid brandId, Uri imageUri, )
        {
            var command = new CreateProduct
        }
    }
}
