using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using CQRS.Infrastructure.Messaging;
using ParcelTracking.ReadModel;
using ParcelTracking.Parsers;
using HtmlAgilityPack;

namespace ParcelTracking.Trackers
{
    public class EmmsTracker : IParcelTracker
    {
        private const string name = "Emms";
        private const string Provider = "auexp";
        private const string Type = "1000";
        private const string BaseAddress = @"http://120.25.248.148";
        private const string RequestUri = @"/cgi-bin/GInfo.dll?EmmisTrack";

        public string Track(string trackNumber)
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

                //todo::make async http request to make it more efficient?
                //Get the Response
                var response = httpClient.PostAsync(RequestUri, formContent).Result;

                //Check the track number if not match the provided number then it is an error.
                var htmlAsString = response.Content.ReadAsStringAsync().Result;

                return htmlAsString;
            }
        }

        public string Name
        {
            get { return _name; }
        }
    }
}