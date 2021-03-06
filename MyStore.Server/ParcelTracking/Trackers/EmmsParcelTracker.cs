﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using CQRS.Infrastructure.Messaging;
using ParcelTracking.Commands;
using ParcelTracking.Parsers;
using HtmlAgilityPack;
using System.Threading.Tasks;

namespace ParcelTracking.Trackers
{
    public class EmmsTracker : IParcelTracker
    {
        private const string _name = "Emms";
        private const string Provider = "auexp";
        private const string Type = "1000";
        private const string BaseAddress = @"http://120.25.248.148";
        private const string RequestUri = @"/cgi-bin/GInfo.dll?EmmisTrack";

        private readonly ICommandBus _commandBus;

        public EmmsTracker(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public async Task TrackAsync(Guid parcelId, string trackingNumber)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(BaseAddress);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("w", Provider),
                    new KeyValuePair<string, string>("cmodel", string.Empty),
                    new KeyValuePair<string, string>("cno", trackingNumber),
                    new KeyValuePair<string, string>("ntype", Type)
                });

                //Get the Response
                var response = await httpClient.PostAsync(RequestUri, formContent);

                //Check the track number if not match the provided number then it is an error.
                var htmlAsString = response.Content.ReadAsStringAsync().Result;

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlAsString);

                var trackInfo = EmmsHtmlParser.GetTrackInfo(htmlDoc);

                _commandBus.Send(new UpdateParcelStatus(parcelId, trackInfo));
            }
        }

        public string Name
        {
            get { return _name; }
        }
    }
}