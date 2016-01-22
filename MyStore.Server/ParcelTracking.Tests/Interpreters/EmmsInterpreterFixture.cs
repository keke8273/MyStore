using FluentAssertions;
using ParcelTracking.Contacts;
using ParcelTracking.Interpreters;
using Xunit;

namespace ParcelTracking.Tests.Interpreters
{
    public class given_warehoused_message_received
    {
        private string _message;
        private EmmsInterpreter _sut;

        public given_warehoused_message_received()
        {
            _message = "快件已入澳洲仓库....";
        }

        [Fact()]
        public void when_interprete_message_then_parcel_warehoused()
        {
            _sut = new EmmsInterpreter();

            var actual = _sut.Translate(_message);

            actual.ShouldBeEquivalentTo(ParcelState.Warehoused);
        }
    }

    public class given_intransit_message_received
    {
        private string _message;
        private EmmsInterpreter _sut;

        public given_intransit_message_received()
        {
            _message = "快件已达澳洲物流中心...等待取件发送......";
        }

        [Fact()]
        public void when_interprete_message_then_parcel_intransit()
        {
            _sut = new EmmsInterpreter();

            var actual = _sut.Translate(_message);

            actual.ShouldBeEquivalentTo(ParcelState.InTransit);
        }
    }

    public class given_custom_message_received
    {
        private string _message;
        private EmmsInterpreter _sut;

        public given_custom_message_received()
        {
            _message = "快件已到达中国.....等待海关检验....";
        }

        [Fact()]
        public void when_interprete_message_then_parcel_awaitcustom()
        {
            _sut = new EmmsInterpreter();

            var actual = _sut.Translate(_message);

            actual.ShouldBeEquivalentTo(ParcelState.AwaitCustom);
        }
    }

    public class given_delivered_message_received
    {
        private string _message;
        private EmmsInterpreter _sut;

        public given_delivered_message_received()
        {
            _message = "投递并签收，签收人：他人收 家人";
        }

        [Fact()]
        public void when_interprete_message_then_parcel_delivered()
        {
            _sut = new EmmsInterpreter();

            var actual = _sut.Translate(_message);

            actual.ShouldBeEquivalentTo(ParcelState.Delivered);
        }
    }

    public class given_unsuccessful_message_received
    {
        private string _message;
        private EmmsInterpreter _sut;

        public given_unsuccessful_message_received()
        {
            _message = "快件派送不成功(因无法联系到收方客户),正在处理中,待再次派送";
        }

        [Fact()]
        public void when_interprete_message_then_parcel_delivered()
        {
            _sut = new EmmsInterpreter();

            var actual = _sut.Translate(_message);

            actual.ShouldBeEquivalentTo(ParcelState.Error);
        }
    }
}
