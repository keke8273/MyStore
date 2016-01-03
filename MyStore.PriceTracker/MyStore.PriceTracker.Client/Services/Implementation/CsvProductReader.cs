using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using Store.Dto;

namespace MyStore.PriceTracker.Client.Services.Implementation
{
    public class CsvProductReader : IProductReader
    {
        private readonly string _path;

        public CsvProductReader(string path)
        {
            _path = path;
        }

        public IEnumerable<SourceBasedProductDto> GetProductInfo()
        {
            using (var csvHelper = new CsvReader(File.OpenText(_path)))
            {
                csvHelper.Configuration.RegisterClassMap<ProductMap>();
                return csvHelper.GetRecords<SourceBasedProductDto>().ToList();
            }
        }
    }

    public sealed class ProductMap : CsvClassMap<SourceBasedProductDto>
    {
        public ProductMap()
        {
            Map(m => m.Brand).Name("brandName");
            Map(m => m.Categories).Name("category").ConvertUsing(ConveterCategory);
            Map(m => m.Name).Name("name");
            Map(m => m.RetailPrice).ConvertUsing(row => row.GetField<Decimal>("price") + row.GetField<Decimal>("save"));
            Map(m => m.Price).Name("price");
            Map(m => m.ImageUrl).Name("image-src");
            Map(m => m.IsAvailableOnline).Name("available").TypeConverterOption(true, "InStock").TypeConverterOption(false, "OutOfStock");           
        }

        private IEnumerable<string> ConveterCategory(ICsvReaderRow row)
        {
            var brandName = row.GetField("brandName");
            var category = row.GetField("category");
            if (string.IsNullOrEmpty(category))
                return new String[] {};

            return category.Replace(brandName, "").Trim().Split(',', '&');
        }
    }
}
