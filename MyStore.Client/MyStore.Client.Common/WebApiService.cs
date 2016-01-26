using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ParcelTracking.Dto;
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

        //Subscription
        public async Task<Guid> SubscribeParcelTrackingAsync(SubscriptionDto dto)
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


        //Parcel
        public async Task<ParcelStatusDto> GetParcelStatusAsync(Guid id)
        {
            using (var client = new HttpClient())
            {
                SetupHttpClient(client);

                var response = await client.GetAsync(@"api/parcel/" + id);

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<ParcelStatusDto>();

                return null;
            }
        }

        public async Task<Guid> FindOrCreateParcelAsync(string expressProvider, string trackingNumber)
        {
            using (var client = new HttpClient())
            {
                SetupHttpClient(client);

                var response = await client.GetAsync(@"api/parcel/" + expressProvider + "/" + trackingNumber);

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<Guid>();
                
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    response = await client.PostAsJsonAsync(@"api/parcel", new ParcelDto
                    {
                        ExpressionProvider = expressProvider,
                        TrackingNumber = trackingNumber
                    });

                    if(response.IsSuccessStatusCode)
                        return await response.Content.ReadAsAsync<Guid>();                        
                }

                return Guid.Empty;
            }
        }

        //User
        public async Task<Guid> Login()
        {
            using (var client = new HttpClient())
            {
                SetupHttpClient(client);
            }

            //dummy implementation to be replaced 
            return Guid.NewGuid();
        }

        private void SetupHttpClient(HttpClient client)
        {
            client.BaseAddress = new Uri(_baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaType));
        }
    }
}
