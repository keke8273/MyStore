using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using HtmlAgilityPack;
using ParcelTracking.Parsers;
using Xunit;

namespace ParcelTracking.Tests.Parsers
{
    public class given_ems_success
    {
        private readonly HtmlDocument _htmlDoc;

        public given_ems_success()
        {
            string filePath = Directory.GetCurrentDirectory() + @"\TestData\ems_success.html";

            _htmlDoc = new HtmlDocument();
            _htmlDoc.Load(filePath, Encoding.GetEncoding(936));
        }

        [Fact()]
        public void when_parse_trackInfo_then_result_correct()
        {
            var expected = new TrackInfo
            {
                TrackingNumber = "H6000042536",
                Origin = "澳洲",
                Destination = "四川省",
                ChineseExpressProvider = "EMS",
                ChineseExpressProviderTrackingNumber = "BE993634263AU",
                Messages = new List<TrackMessage>
                {
                new TrackMessage
                {
                    TimeStamp = new DateTime(2015, 09, 19, 20, 30, 0),
                    Location = "澳洲",
                    Message = "快件已入澳洲仓库...."
                },
                new TrackMessage
                {
                    TimeStamp = new DateTime(2015, 09, 21, 22, 21, 0),
                    Location = "澳洲",
                    Message = "快件已达澳洲物流中心...等待取件发送......"
                },
                new TrackMessage
                {
                    TimeStamp = new DateTime(2015, 09, 22, 08, 38, 0),
                    Location = "澳洲",
                    Message = "快件已送达国际航运中心...等待发往中国......"
                },
                new TrackMessage
                {
                    TimeStamp = new DateTime(2015, 09, 22, 11, 55, 0),
                    Location = "澳洲",
                    Message = "恒通四十五批....已发往中国....."
                },
                new TrackMessage
                {
                    TimeStamp = new DateTime(2015, 09, 23, 14, 29, 0),
                    Location = "中国",
                    Message = "快件已到达中国.....等待海关检验......"
                },
                new TrackMessage
                {
                    TimeStamp = new DateTime(2015, 09, 25, 20, 28, 0),
                    Location = "广州市",
                    Message = "广东省邮政速递物流有限公司广州分公司快件监管中心国内物流营业部已收件，（揽投员姓名：陈振宇;联系电话：）"
                },
                new TrackMessage
                {
                    TimeStamp = new DateTime(2015, 09, 26, 18, 20, 0),
                    Location = "广州市",
                    Message = "离开广州市 发往成都市"
                },
                new TrackMessage
                {
                    TimeStamp = new DateTime(2015, 09, 28, 12, 06, 0),
                    Location = "成都市",
                    Message = "离开成都集散航空转运中心 发往成都市邮政速递物流有限公司汪家拐揽投站"
                },
                new TrackMessage
                {
                    TimeStamp = new DateTime(2015, 09, 28, 13, 20, 0),
                    Location = "成都市",
                    Message = "成都市邮政速递物流有限公司汪家拐揽投站 安排投递，预计21:00:00前投递（投递员姓名：高学义;联系电话：18981970097）"
                },
                new TrackMessage
                {
                    TimeStamp = new DateTime(2015, 09, 28, 15, 24, 0),
                    Location = "成都市",
                    Message = "投递并签收，签收人：单位收发章 单位收发章"
                },
            }
            };

            var actual = EmmsHtmlParser.GetTrackInfo(_htmlDoc);

            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
