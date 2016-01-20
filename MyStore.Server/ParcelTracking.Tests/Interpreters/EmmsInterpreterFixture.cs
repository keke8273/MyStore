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
        private TrackDetail _trackDetail;

        public given_warehoused_trackDetail()
        {
            _trackDetail = new TrackDetail
            {
                TimeStamp = new DateTime(2016, 01, 04, 20, 30, 00),
                Location = "澳洲",
                Message = "快件已入澳洲仓库....",
            };
        }

        [Fact()]
        public void when_interprete_trackDetail_then_update_parcel_state()
        {
            var expected = new UpdateParcelStatus
            {
                TrackingNumber = "H6000042536",
                Origin = "澳洲",
                Destination = "四川省",
                ChineseExpressProvider = "EMS",
                ChineseExpressProviderTrackingNumber = "BE993634263AU",
            };

            _sut = new EmmsHtmlParser();

            var actual = _sut.GetTrackInfo(_htmlDoc);

            actual.ShouldBeEquivalentTo(expected);
        }

    }
}
