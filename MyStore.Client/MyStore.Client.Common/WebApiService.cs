using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Subscription.Dto;

namespace MyStore.Client.Common
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

        //Register new parcel
        public async Task<Guid> SubscribeParcelTracking(ParcelTrackingSubscriptionDto dto)
        {
            using (var client = new HttpClient())
            {
                SetupHttpClient(client);

                var response = await client.PostAsJsonAsync(@"api/subscription", dto);

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<Guid>();

                return Guid.Empty;
            }
        }

        //Check for parcel status
        public async Task<ParcelStatusDto> GetParcelStatus(Guid id)
        {
            using (var client = new HttpClient())
            {
                SetupHttpClient(client);

                var response = await client.GetAsync(@"api/parcel/" + id);
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
