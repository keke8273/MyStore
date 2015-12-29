using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using CQRS.Infrastructure.Messaging;
using CQRS.Infrastructure.Utils;
using Product.Commands;
using Product.Dto;
using Product.ReadModel;

namespace MyStore.Server.WebApi.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductDao _productDao;
        private readonly ICommandBus _bus;

        public ProductController(IProductDao productDao, ICommandBus bus)
        {
            _bus = bus;
            _productDao = productDao;
        }

        [ResponseType(typeof(ProductDto))]
        public async Task<IHttpActionResult> GetProduct(Guid id)
        {
            var product = _productDao.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Product.ReadModel.Product, ProductDto>(product));
        }

        [ResponseType(typeof(Guid))]
        public async Task<IHttpActionResult> LocateProduct(string name)
        {
            var productId = _productDao.LocateProduct(name);

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
