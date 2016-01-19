using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using CQRS.Infrastructure.Messaging;
using ParcelTracking.ReadModel;
using ParcelTracking.Parsers;
using HtmlAgilityPack;
using System.Threading.Tasks;
using ParcelTracking.Events;

namespace ParcelTracking.Trackers
{
    public class EmmsTracker : BaseParcelTracker
    {
        private const string _name = "Emms";
        private const string Provider = "auexp";
        private const string Type = "1000";
        private const string BaseAddress = @"http://120.25.248.148";
        private const string RequestUri = @"/cgi-bin/GInfo.dll?EmmisTrack";

        public EmmsTracker(IEventBus eventBus) : base(eventBus)
        {
        }

        public async Task TrackAsync(Parcel parcel)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(BaseAddress);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("w", Provider),
                    new KeyValuePair<string, string>("cmodel", string.Empty),
                    new KeyValuePair<string, string>("cno", parcel.TrackingNumber),
                    new KeyValuePair<string, string>("ntype", Type)
                });

                //Get the Response
                var response = await httpClient.PostAsync(RequestUri, formContent);

                //Check the track number if not match the provided number then it is an error.
                var htmlAsString = response.Result.Content.ReadAsStringAsync().Result;

                var htmlDoc = new HtmlDocument();
                htmlDoc.Load(htmlAsString, Encoding.GetEncoding(936));

                var trackInfo = EmmsHtmlParser.GetTrackInfo(htmlDoc);

                var parcelEvents = BuildParcelEvents(parcel, trackInfo);

                _eventBus.Publish(parcelEvents);
            }
        }

        private IEnumerable<ParcelEvent> BuildParcelEvents(Parcel parcel, TrackInfo trackInfo)
        {
            var events = new List<ParcelEvent>();

            if(trackInfo.Origin != parcel.Origin)
                events.Add(new ParcelOriginUpdated(parcel.Id){Origin == trackInfo.Origin});

            if(trackInfo.Destination != parcel.Destination)
                events.Add(new ParcelDestinationUpdated(parcel.Id){Destination == trackInfo.Destination});

            if(trackInfo.ChineseExpressProvider != parcel.ChineseExpressProvider)
                events.Add(new ChineseExpressProviderUpdated(trackInfo.ChineseExpressProvider));

            if (trackInfo.ChineseExpressProviderTrackingNumber != parcel.ChineseExpressProviderTrackingNumber)
                events.Add(new ChineseExpressProviderTrackingNumberUpdated(trackInfo.ChineseExpressProviderTrackingNumber));

            for (int i = parcel.MessageReceived; i < trackInfo.TrackDetails.Count; i++)
            {
                events.Add(EmmsInterpreter.Translate(trackInfo.TrackDetails[i]));
            }

            return events;
        }

        public string Name
        {
            get { return _name; }
        }
    }
}