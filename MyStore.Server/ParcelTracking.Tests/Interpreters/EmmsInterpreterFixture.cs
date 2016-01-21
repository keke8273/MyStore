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
        public void when_interprete_trackDetail_then_update_parcel_state()
        {
            _sut = new EmmsInterpreter();

            var actual = _sut.Translate(_message);

            actual.ShouldBeEquivalentTo(ParcelState.Warehoused);
        }

    }
}
