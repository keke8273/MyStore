using System;
using Moq;
using MyStore.Common;
using MyStore.Common.Utils;
using ProductTracking.Contracts.Commands;
using ProductTracking.Contracts.Events;
using ProductTracking.Handlers;
using Xunit;
using CQRS.Infrastructure.Utils;

namespace ProductTracking.Tests
{
    public class given_new_online_availability
    {
        private static readonly Guid ProductId = Guid.NewGuid();
        private static readonly Guid ProductSourceId = Guid.NewGuid();
        private readonly Mock<IDateTimeService> _dateTimeSerive;
        private DateTime _testTime = DateTime.UtcNow;
        private EventSourcingTestHelper<ProductOnlineAvailability> sut;
        private const bool initialAvailability = false;

        public given_new_online_availability()
        {
            _dateTimeSerive = new Mock<IDateTimeService>();
            _dateTimeSerive.Setup(x => x.GetCurrentDateTimeUtc()).Returns(_testTime);
            DateTimeUtil.RegisterDateTimeService(_dateTimeSerive.Object);

            sut = new EventSourcingTestHelper<ProductOnlineAvailability>();
            sut.Setup(new OnlineAvailabilityCommandHandler(sut.Repository));
        }

        [Fact]
        public void when_updating_availability_then_create_new_source()
        {
            const bool expected = initialAvailability;
            sut.When(new UpdateProductOnlineAvailability { IsAvailable = expected, ProductId = ProductId, ProductSourceId = ProductSourceId });

            var @event = sut.ThenHasOne<OnlineAvailabilityUpdated>();
            Assert.Equal(ProductId, @event.SourceId);
            Assert.Equal(ProductSourceId, @event.ProductSourceId);
            Assert.Equal(expected, @event.NewAvailability);
            Assert.Equal(false, @event.PreviousAvailability);
            Assert.Equal(_testTime, @event.TimeStamp);
        }
    }

    public class given_existing_online_availability
    {
        private static readonly Guid ProductId = Guid.NewGuid();
        private static readonly Guid ProductSourceId = Guid.NewGuid();
        private readonly Mock<IDateTimeService> _dateTimeSerive;
        private DateTime _testTime = DateTime.UtcNow;
        private EventSourcingTestHelper<ProductOnlineAvailability> sut;

        public given_existing_online_availability()
        {
            _dateTimeSerive = new Mock<IDateTimeService>();
            _dateTimeSerive.Setup(x => x.GetCurrentDateTimeUtc()).Returns(_testTime);
            DateTimeUtil.RegisterDateTimeService(_dateTimeSerive.Object);

            sut = new EventSourcingTestHelper<ProductOnlineAvailability>();
            sut.Setup(new OnlineAvailabilityCommandHandler(sut.Repository));

            sut.Given(
                new OnlineAvailabilityUpdated(_testTime.AddHours(-1))
                {
                    SourceId = ProductId,
                    NewAvailability = true,
                    PreviousAvailability = false,
                    ProductSourceId = ProductSourceId
                }
                );
        }

        [Fact]
        public void when_availability_from_same_source_changed_then_update_availability()
        {
            const bool expectedAvailability = false;
            sut.When(new UpdateProductOnlineAvailability { IsAvailable = false, ProductId = ProductId, ProductSourceId = ProductSourceId });

            var @event = sut.ThenHasOne<OnlineAvailabilityUpdated>();
            Assert.Equal(ProductId, @event.SourceId);
            Assert.Equal(ProductSourceId, @event.ProductSourceId);
            Assert.Equal(expectedAvailability, @event.NewAvailability);
            Assert.Equal(true, @event.PreviousAvailability);
            Assert.Equal(_testTime, @event.TimeStamp);
        }

        [Fact]
        public void when_availability_from_another_source_changed_then_add_source()
        {
            Guid anotherProductSourceId = Guid.NewGuid();
            const bool expectedAvailability = false;
            sut.When(new UpdateProductOnlineAvailability { IsAvailable = false, ProductId = ProductId, ProductSourceId = anotherProductSourceId });

            var @event = sut.ThenHasOne<OnlineAvailabilityUpdated>();
            Assert.Equal(ProductId, @event.SourceId);
            Assert.Equal(anotherProductSourceId, @event.ProductSourceId);
            Assert.Equal(expectedAvailability, @event.NewAvailability);
            Assert.Equal(false, @event.PreviousAvailability);
            Assert.Equal(_testTime, @event.TimeStamp);
        }
    }
}
