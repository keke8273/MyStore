using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using MyStore.Common;

namespace ParcelTracking.Parsers
{
    public static class EmmsHtmlParser
    {
        public static TrackInfo GetTrackInfo(HtmlDocument htmlDoc)
        {
            var trackInfo = new TrackInfo();

            var trackInfoNode = htmlDoc.DocumentNode.SelectSingleNode("//td[@id='theTrackInfo']");
            trackInfo.TrackingNumber = trackInfoNode.SelectSingleNode("//span[@id='HeaderNum']").InnerText.Extract(@"([^\uff1a]+)$");
            trackInfo.Origin = trackInfoNode.SelectSingleNode("//span[@id='HeaderFrom']").InnerText.Extract(@"([^\uff1a]+)$");
            trackInfo.Destination = trackInfoNode.SelectSingleNode("//span[@id='HeaderDes']").InnerText.Extract(@"([^\uff1a]+)$");
            trackInfo.ChineseExpressProvider = trackInfoNode.SelectSingleNode("//span[@id='HeaderEmsKind']").InnerText.Extract(@"\[(.*?)\]");
            trackInfo.ChineseExpressProviderTrackingNumber = trackInfoNode.SelectSingleNode("//span[@id='HeaderEmsKind']").InnerText.Extract(@"([^\]]+)$").Trim();

            return trackInfo;
        }

        public static IEnumerable<TrackDetail> GetTrackDetail(HtmlDocument htmlDoc)
        {
            var trackDetails = new List<TrackDetail>();

            var trackDetailTable = htmlDoc.DocumentNode.SelectSingleNode("//table[@id='oTHtable']");

            var trackDetailRows = trackDetailTable.SelectSingleNode("tbody").SelectNodes("./tr");

            for (int i = 1; i < trackDetailRows.Count; i++)
            {
                var cells = trackDetailRows[i].SelectNodes("td");

                trackDetails.Add(new TrackDetail
                {
                    TimeStamp = DateTime.Parse(cells[0].InnerText),
                    Location = cells[1].InnerText.Trim(),
                    Message = cells[2].InnerText.Trim()
                });
            }

            return trackDetails;
        }
    }
}
