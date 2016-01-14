using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using MyStore.Common;

namespace ParcelTracking.Parsers
{
    public class EmmsHtmlParser
    {
        public TrackInfo GetTrackInfo(HtmlDocument htmlDoc)
        {
            var trackInfo = new TrackInfo();

            trackInfo.TrackingNumber = htmlDoc.DocumentNode.SelectSingleNode("//td[@id='theTrackInfo']").SelectSingleNode("//span[@id='HeaderNum']").InnerText.Extract(@"[^\uff1a]+$");

            return trackInfo;
        }

        public IEnumerable<TrackDetail> GetTrackDetail(HtmlDocument htmlAsString)
        {
            throw new NotImplementedException();
        }
    }
}
