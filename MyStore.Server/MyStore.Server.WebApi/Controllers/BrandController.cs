using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using CQRS.Infrastructure.Utils;
using Store.Dto;
using Store.ReadModel;

namespace MyStore.Server.WebApi.Controllers
{
    [RoutePrefix("api/brand")]
    public class BrandController : ApiController
    {
        private readonly IBrandDao _brandDao;

        public BrandController(IBrandDao brandDao)
        {
            _brandDao = brandDao;
        }

        public IEnumerable<BrandDto> GetAll()
        {
            var brands = _brandDao.GetAllBrands();

            return Mapper.Map<IEnumerable<Brand>, IEnumerable<BrandDto>>(brands);
        }

        [HttpPost]
        public Guid CreateBrand(BrandDto brandDto)
        {
            var brand = new Brand
            {
                Id = GuidUtil.NewSequentialId(),
                Name = brandDto.Name
            };

            return _brandDao.CreateBrand(brand);
        }
    }
}