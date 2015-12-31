using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using CQRS.Infrastructure.Messaging;
using CQRS.Infrastructure.Utils;
using Store.Commands;
using Store.Dto;
using Store.ReadModel;

namespace MyStore.Server.WebApi.Controllers
{
    [RoutePrefix("api/product")]
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

        [Route("")]
        [HttpPost]
        [ResponseType(typeof(Guid))]
        public async Task<IHttpActionResult> CreateProduct(ProductDto product)
        {
            var command = new CreateProduct
            {
                ProductId = GuidUtil.NewSequentialId(),
                ProductName = product.Name,
                BrandId = product.BrandId, 
                ImageUrl = product.ImageUrl
            };

            _bus.Send(command);

            return Ok(command.ProductId);
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateProductInfo(ProductInfoDto status)
        {
            var commands = new List<ICommand>
            {
                new UpdateProductPrice
                {
                    ProductId = status.ProductId,
                    ProductSourceId = status.ProductSourceId,
                    Price = status.Price,
                },
                new UpdateProductOnlineAvailibility
                {
                    ProductId = status.ProductId,
                    ProductSourceId = status.ProductSourceId,
                    IsAvailalbe = status.IsAvailableOnline
                }
            };

            _bus.Send(commands);

            return Ok();
        }
    }
}
