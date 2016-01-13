using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using CQRS.Infrastructure.Messaging;
using ParcelTracking.ReadModel;

namespace ParcelTracking.Trackers
{
    public class EmmsTracker : ParcelTrackerBase
    {
        private const string Name = "EmmsTracker";
        private const string Provider = "auexp";
        private const string Type = "1000";
        private const string BaseAddress = @"http://120.25.248.148";
        private const string RequestUri = @"/cgi-bin/GInfo.dll?EmmisTrack";
        private CancellationTokenSource _cancellationTokenSource;

        public EmmsTracker(IParcelStatusDao parcelStatusDao, ICommandBus commandBus) 
            : base(Name, parcelStatusDao, commandBus)
        {
        }

        public void Track(string trackNumber)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(BaseAddress);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("w", Provider),
                    new KeyValuePair<string, string>("cmodel", string.Empty),
                    new KeyValuePair<string, string>("cno", trackNumber),
                    new KeyValuePair<string, string>("ntype", Type)
                });


                //Get the Response
                var response = httpClient.PostAsync(RequestUri, formContent).Result;

                //Check the track number if not match the provided number then it is an error.
                var htmlAsString = response.Content.ReadAsStringAsync().Result;

                //Check the track number if not match the provided number then it is an error.

                //Parse the "theTrackInfo" section for origin, destination, chineseTrackNumber, chineseExpProvider

                //Parse the oDetail section for details.


            }
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }
    }
}