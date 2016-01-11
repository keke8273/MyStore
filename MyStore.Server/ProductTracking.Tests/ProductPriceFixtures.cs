using System;
using Moq;
using MyStore.Common;
using MyStore.Common.Utils;
using ProductTracking.Commands;
using ProductTracking.Events;
using ProductTracking.Handlers;
using Xunit;

namespace ProductTracking.Tests
{
    public class given_new_product_price
    {
        private static readonly Guid ProductId = Guid.NewGuid();
        private static readonly Guid ProductSourceId = Guid.NewGuid();
        private readonly Mock<IDateTimeService> _dateTimeSerive;
        private DateTime _testTime = DateTime.UtcNow;
        private EventSourcingTestHelper<ProductPrice> sut;
        private const Decimal initialPrice = 1.0M;

        public given_new_product_price()
        {
            _dateTimeSerive = new Mock<IDateTimeService>();
            _dateTimeSerive.Setup(x => x.GetCurrentDateTimeUtc()).Returns(_testTime);
            sut = new EventSourcingTestHelper<ProductPrice>();
            sut.Setup(new ProductPriceCommandHandler(sut.Repository, _dateTimeSerive.Object));
        }

        [Fact]
        public void when_updating_price_then_add_new_product_price()
        {
            const decimal expectedPrice = initialPrice;
            sut.When(new UpdateProductPrice { Price = expectedPrice, ProductId = ProductId, ProductSourceId = ProductSourceId });

            var @event = sut.ThenHasOne<ProductPriceUpdated>();
            Assert.Equal(ProductId, @event.SourceId);
            Assert.Equal(ProductSourceId, @event.ProductSourceId);
            Assert.Equal(expectedPrice, @event.NewPrice);
            Assert.Equal(0, @event.PreviousPrice);
            Assert.Equal(_testTime, @event.TimeStamp);
        }

        //[Fact]
        //public void when_updating_online_availability_then_add_new_product_online_availability()
        //{
        //    const bool expectedAvailability = true;
        //    sut.When(new UpdateProductOnlineAvailability { IsAvailable = true, ProductId = ProductId, ProductSourceId = ProductSourceId });

        //    var @event = sut.ThenHasOne<OnlineAvailabilityUpdated>();
        //    Assert.Equal(ProductId, @event.SourceId);
        //    Assert.Equal(ProductSourceId, @event.ProductSourceId);
        //    Assert.Equal(expectedAvailability, @event.NewAvailability);
        //    Assert.Equal(false, @event.PreviousAvailability);
        //    Assert.Equal(_testTime, @event.TimeStamp);
        //}
    }

    public class given_existing_product_price
    {
        private static readonly Guid ProductId = Guid.NewGuid();
        private static readonly Guid ProductSourceId = Guid.NewGuid();
        private readonly Mock<IDateTimeService> _dateTimeSerive;
        private DateTime _testTime = DateTime.UtcNow;
        private EventSourcingTestHelper<ProductPrice> sut;
        private const Decimal initialPrice = 1.0M;

        public given_existing_product_price()
        {
            _dateTimeSerive = new Mock<IDateTimeService>();
            _dateTimeSerive.Setup(x => x.GetCurrentDateTimeUtc()).Returns(_testTime);
            sut = new EventSourcingTestHelper<ProductPrice>();
            sut.Setup(new ProductPriceCommandHandler(sut.Repository, _dateTimeSerive.Object));

            sut.Given(
                new ProductPriceUpdated
                {
                    SourceId = ProductId,
                    NewPrice = initialPrice,
                    PreviousPrice = 0,
                    ProductSourceId = ProductSourceId,
                    TimeStamp = _testTime.AddHours(-1)
                }
                );
        }

        [Fact]
        public void when_price_from_same_source_changed_then_update_price()
        {
            var expectedPrice = initialPrice + 1;
            sut.When(new UpdateProductPrice { Price = expectedPrice, ProductId = ProductId, ProductSourceId = ProductSourceId });

            var @event = sut.ThenHasOne<ProductPriceUpdated>();
            Assert.Equal(ProductId, @event.SourceId);
            Assert.Equal(ProductSourceId, @event.ProductSourceId);
            Assert.Equal(expectedPrice, @event.NewPrice);
            Assert.Equal(initialPrice, @event.PreviousPrice);
            Assert.Equal(_testTime, @event.TimeStamp);
        }

        [Fact]
        public void when_price_from_another_source_changed_then_add_new_source()
        {
            Guid anotherProductSourceId = Guid.NewGuid();

            var expectedPrice = initialPrice;
            sut.When(new UpdateProductPrice{ Price = expectedPrice, ProductId = ProductId, ProductSourceId = anotherProductSourceId});

            var @event = sut.ThenHasOne<ProductPriceUpdated>();
            Assert.Equal(ProductId, @event.SourceId);
            Assert.Equal(anotherProductSourceId, @event.ProductSourceId);
            Assert.Equal(expectedPrice, @event.NewPrice);
            Assert.Equal(0, @event.PreviousPrice);
            Assert.Equal(_testTime, @event.TimeStamp);
        }
    }
}
