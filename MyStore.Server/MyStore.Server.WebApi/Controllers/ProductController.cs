using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CQRS.Infrastructure.Messaging;
using CQRS.Infrastructure.Utils;
using Product.Commands;
using Product.ReadModel;

namespace MyStore.Server.WebApi.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductDao productDao;
        private readonly ICommandBus _bus;

        public ProductController(IProductDao productDao, ICommandBus bus)
        {
            _bus = bus;
            productDao = productDao;
        }

        [ResponseType(typeof(Product.ReadModel.Product))]
        public async Task<IHttpActionResult> GetProduct(Guid productId)
        {
            var product = productDao.GetProduct(productId);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [ResponseType(typeof(Guid))]
        public async Task<IHttpActionResult> LocateProduct(string name)
        {
            var productId = productDao.LocateProduct(name);

            if (!productId.HasValue)
            {
                return NotFound();
            }

            return Ok(productId.Value);
        }

        [ResponseType(typeof(Guid))]
        public async Task<IHttpActionResult> CreateProduct(string name, Guid brandId, Uri imageUri)
        {
            var command = new CreateProduct
            {
                ProductId = GuidUtil.NewSequentialId(), 
                ProductName = name,
                BrandId = brandId, 
                ImageUri = imageUri
            };

            _bus.Send(command);

            return Ok(command.ProductId);
        }
    }
}
