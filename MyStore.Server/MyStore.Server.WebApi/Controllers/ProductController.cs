using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using CQRS.Infrastructure.Messaging;
using CQRS.Infrastructure.Utils;
using Store;
using Store.Commands;
using Store.Dto;
using Store.ReadModel;
using Product = Store.ReadModel.Product;

namespace MyStore.Server.WebApi.Controllers
{
    [RoutePrefix("api/productDto")]
    public class ProductController : ApiController
    {
        private readonly IProductDao _productDao;
        private readonly ICommandBus _bus;
        private readonly IEventBus _eventBus;

        private ProductService _productService;

        private ProductService Service
        {
            get { return _productService ?? (_productService = new ProductService(_eventBus)); }
        }

        public ProductController(IProductDao productDao, ICommandBus bus, IEventBus eventBus)
        {
            _bus = bus;
            _eventBus = eventBus;
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

            return Ok(Mapper.Map<Product, ProductDto>(product));
        }

        [Route("{name}")]
        [HttpGet]
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

        [HttpPost]
        public async Task<IHttpActionResult> UpdateProduct(SourceBasedProductDto productDto)
        {
            var productId = _productDao.LocateProduct(productDto.Name);

            if (!productId.HasValue)
                productId = Service.CreateProduct(Mapper.Map<ProductInfo>(productDto));

            var productSource = Service.FindSource(productDto.SourceName) ?? Service.CreateSource(productDto.SourceName);

            var commands = new List<ICommand>
            {
                new UpdateProductPrice
                {
                    ProductId = productId.Value,
                    ProductSourceId =productSource.Id,
                    Price = productDto.Price,
                },
                new UpdateProductOnlineAvailibility
                {
                    ProductId = productId.Value,
                    ProductSourceId = productSource.Id,
                    IsAvailalbe = productDto.IsAvailableOnline
                }
            };

            _bus.Send(commands);

            return Ok();
        }
    }
}
