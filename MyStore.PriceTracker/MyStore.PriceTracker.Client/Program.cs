using System;
using System.Configuration;
using System.IO;
using System.Linq;
using MyStore.PriceTracker.Client.PriceTrackers;
using MyStore.PriceTracker.Client.Services;
using MyStore.PriceTracker.Client.Services.Implementation;
using Store.Dto;

namespace MyStore.PriceTracker.Client
{
    class Program
    {
        private static WebApiService _webApiService;
        private static ChemistWarehouseOnlineStoreTracker _chemistWarehouseOnlineStoreTracker;
        static void Main(string[] args)
        {

            _webApiService = new WebApiService("http://localhost:3400", "application/json");
            _chemistWarehouseOnlineStoreTracker = new ChemistWarehouseOnlineStoreTracker(_webApiService);

            while (true)
            {
                _chemistWarehouseOnlineStoreTracker.TrackPrice();

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
