using AutoMapper;
using Store.Dto;
using Store.ReadModel;

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
            Mapper.CreateMap<Product, ProductDto>();
            Mapper.CreateMap<Brand, BrandDto>();
        }
    }
}