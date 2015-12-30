﻿using System;
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
    }
}
