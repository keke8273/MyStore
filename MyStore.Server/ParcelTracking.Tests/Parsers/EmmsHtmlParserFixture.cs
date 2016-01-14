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
        private EmmsHtmlParser _sut;
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
                ParcelCount = 1
            };

            _sut = new EmmsHtmlParser();

            var actual = _sut.GetTrackInfo(_htmlDoc);

            actual.ShouldBeEquivalentTo(expected);
        }

        [Fact()]
        public void GetTrackDetailTest()
        {
            Assert.True(false, "not implemented yet");
        }
    }
}
