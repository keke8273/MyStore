using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Store.Dto;

namespace MyStore.PriceTracker.Client.Services
{
    public class WebApiService
    {
        private readonly string _baseAddress;
        private readonly string _mediaType;

        public WebApiService(string baseAddress, string mediaType)
        {
            _baseAddress = baseAddress;
            _mediaType = mediaType;
        }

        public async Task<Guid> LoadProductAsync(string name)
        {
            using (var client = new HttpClient())
            {
                SetupHttpClient(client);
                var response = await client.GetAsync(@"api/product" + name);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Guid>();
                }

                return Guid.Empty;
            }
        }

        public async Task<Guid> CreateProductAsync(ProductDto productDto)
        {
            using (var client = new HttpClient())
            {
                SetupHttpClient(client);
                var response = await client.PostAsJsonAsync(@"api/product", productDto);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Guid>();
                }

                return Guid.Empty;
            }
        }

        public async void UpdateProductStatusAsync(SourceBasedProductDto sourceBasedProductDto)
        {
            using (var client = new HttpClient())
            {
                SetupHttpClient(client);
                var response = await client.PostAsJsonAsync(@"api/product", sourceBasedProductDto);

                if (response.IsSuccessStatusCode)
                    return;
            }
        }

        private void SetupHttpClient(HttpClient client)
        {
            client.BaseAddress = new Uri(_baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaType));
        }
    }
}
