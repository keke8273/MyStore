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

            var trackInfoNode = htmlDoc.DocumentNode.SelectSingleNode("//td[@id='theTrackInfo']");
            trackInfo.TrackingNumber = trackInfoNode.SelectSingleNode("//span[@id='HeaderNum']").InnerText.Extract(@"[^\uff1a]+$");
            trackInfo.Origin = trackInfoNode.SelectSingleNode("//span[@id='HeaderFrom']").InnerText.Extract(@"[^\uff1a]+$");
            trackInfo.Destination = trackInfoNode.SelectSingleNode("//span[@id='HeaderDes']").InnerText.Extract(@"[^\uff1a]+$");

            return trackInfo;
        }

        public IEnumerable<TrackDetail> GetTrackDetail(HtmlDocument htmlAsString)
        {
            throw new NotImplementedException();
        }
    }
}
