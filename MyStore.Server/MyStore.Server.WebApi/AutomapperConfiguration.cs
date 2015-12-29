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
                cfg.AddProfile(new ProductProfile());
            });
            
            Mapper.AssertConfigurationIsValid();
        }
    }

    public class ProductProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Product.ReadModel.Product, ProductDto>();
        }
    }
}