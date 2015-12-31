using System;
using System.Configuration;
using System.IO;
using System.Linq;
using MyStore.PriceTracker.Client.Services;
using MyStore.PriceTracker.Client.Services.Implementation;
using Product.Dto;

namespace MyStore.PriceTracker.Client
{
    class Program
    {
        private static WebApiService _webApiService;

        static void Main(string[] args)
        {
            string directory;
            if (args.Any())
                directory = args[0];
            else
                directory = ConfigurationManager.AppSettings["defaultDirectory"];

            var files = Directory.GetFiles(directory);
            
            foreach (var file in files)
            {
                Console.WriteLine(file); 
            }

            _webApiService = new WebApiService("http://localhost:3400", "application/json");

            while (true)
            {
                Console.WriteLine("Enter a file name:");
                
                var fileName = Console.ReadLine();
                var csvReader = new CsvProductReader(Path.Combine(directory, fileName));
                var products = csvReader.GetProducts();
                var brands = _webApiService.GetBrandsAsync().Result.ToList();

                foreach (var product in products)
                {
                    Guid brandId, productId;
                    var brand = brands.FirstOrDefault(b => b.Name == product.Brand);

                    if (brand == null)
                    {
                        brandId = _webApiService.CreateBrandAsync(new BrandDto
                        {
                            Name = product.Brand
                        }).Result;
                        
                        brands.Add(new BrandDto
                        {
                            Id = brandId,
                            Name = product.Brand
                        });
                    }
                    else
                        brandId = brand.Id;

                    productId = _webApiService.LoadProductAsync(product.Name).Result;

                    if (productId == Guid.Empty)
                    {
                        productId = _webApiService.CreateProductAsync(new ProductDto
                        {}).Result;
                    }

                    _webApiService.UpdateProductStatusAsync();
                }

                //Console.WriteLine("Enter a brand name:");
                //var brandName = Console.ReadLine();

                //_webApiService.CreateBrandAsync(new BrandDto {Name = brandName}).Wait();

                //foreach (var brand in _webApiService.GetBrandsAsync().Result)
                //{
                //    Console.WriteLine("{0}, {1}", brand.Id, brand.Name);
                //}
            }
        }
    }
}
