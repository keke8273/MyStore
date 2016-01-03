using System;
using System.Configuration;
using System.IO;
using MyStore.PriceTracker.Client.Services;
using MyStore.PriceTracker.Client.Services.Implementation;

namespace MyStore.PriceTracker.Client.PriceTrackers
{
    public class ChemistWarehouseOnlineStoreTracker : PriceTrackerBase
    {
        private readonly WebApiService _webApiService;

        public ChemistWarehouseOnlineStoreTracker(WebApiService webApiService)
        {
            _webApiService = webApiService;
            SourceName = "ChemistWarehouseOnlineStore";
        }

        public override void TrackPrice()
        {
            string directory = ConfigurationManager.AppSettings["defaultDirectory"];

            var files = Directory.GetFiles(directory);

            foreach (var file in files)
            {
                Console.WriteLine(file);
            }

            Console.WriteLine("Enter a file name:");

            var fileName = Console.ReadLine();
            var csvReader = new CsvProductReader(Path.Combine(directory, fileName));
            var productInfoCollection = csvReader.GetProductInfo();

            foreach (var productInfo in productInfoCollection)
            {
                productInfo.SourceName = SourceName;
                _webApiService.UpdateProductStatusAsync(productInfo);
            }
        }
    }
}
