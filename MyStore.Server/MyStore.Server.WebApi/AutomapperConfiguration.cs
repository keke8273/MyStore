using AutoMapper;
using Product.Dto;

namespace MyStore.Server.WebApi
{
    public static class AutomapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new StoreManagementProfile());
            });
            
            Mapper.AssertConfigurationIsValid();
        }
    }

    public class StoreManagementProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Product.ReadModel.Product, ProductDto>();
            Mapper.CreateMap<Product.ReadModel.Brand, BrandDto>();
        }
    }
}