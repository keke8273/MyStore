﻿using AutoMapper;
using MyStore.Common;
using Store;
using Store.Dto;
using Store.ReadModel;
using Product = Store.ReadModel.Product;

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
            Mapper.CreateMap<Category, string>().ConvertUsing<CategoryStringConverter>();
            Mapper.CreateMap<Product, ProductDto>();
            Mapper.CreateMap<ProductDto, ProductInfo>().Ignore(p => p.Id);
        }
    }

    public class CategoryStringConverter : ITypeConverter<Category, string>
    {
        public string Convert(ResolutionContext context)
        {
            return ((Category) context.SourceValue).Name;
        }
    }
}