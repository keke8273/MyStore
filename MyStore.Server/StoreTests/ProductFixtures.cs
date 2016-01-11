using System;
using Moq;
using MyStore.Common;
using MyStore.Common.Utils;
using Store.Commands;
using Store.Contracts;
using Store.Handlers;
using Xunit;
namespace Store.Tests
{
    public class given_no_product
    {
        private static readonly Guid ProductId = Guid.NewGuid();
        private const string BrandName = "TestBrandName";
        private const string ProductName = "TestProductName";
        private static readonly Uri ImageUrl = new Uri("http://TestProductImageUrl");
        private readonly Mock<IDateTimeService> _dateTimeSerive;
        private DateTime _now = DateTime.UtcNow;
        private EventSourcingTestHelper<Product> sut; 

        public given_no_product()
        {
            _dateTimeSerive = new Mock<IDateTimeService>();
            _dateTimeSerive.Setup(x => x.GetCurrentDateTimeUtc()).Returns(_now);
            sut = new EventSourcingTestHelper<Product>();
            sut.Setup(new ProductCommandHandler(sut.Repository, _dateTimeSerive.Object));
        }        
    }

    public class given_new_product
    {
        private static readonly Guid ProductId = Guid.NewGuid();
        private static readonly string BrandName = "TestBrandName";
        private static readonly Guid ProductSourceId = Guid.NewGuid();
        private static readonly string ProductName = "TestProductName";
        private static readonly string ImageUrl = "http://TestProductImageUrl";
        private readonly Mock<IDateTimeService> _dateTimeSerive;
        private DateTime _testTime = DateTime.UtcNow;
        private EventSourcingTestHelper<Product> sut;
        private const Decimal initialPrice = 1.0M;

        public given_new_product()
        {
            _dateTimeSerive = new Mock<IDateTimeService>();
            _dateTimeSerive.Setup(x => x.GetCurrentDateTimeUtc()).Returns(_testTime);
            sut = new EventSourcingTestHelper<Product>();
            sut.Setup(new ProductCommandHandler(sut.Repository, _dateTimeSerive.Object));

            sut.Given(
                new ProductCreated
                {
                    Brand = BrandName,
                    SourceId = ProductId,
                    Name = ProductName,
                    ImageUrl = ImageUrl
                });
        }

        [Fact]
        public void when_updating_price_then_add_new_product_price()
        {
            const decimal expectedPrice = initialPrice;
            sut.When(new UpdateProductPrice{Price = expectedPrice, ProductId = ProductId, ProductSourceId = ProductSourceId});

            var @event = sut.ThenHasOne<PriceUpdated>();
            Assert.Equal(ProductId, @event.SourceId);
            Assert.Equal(ProductSourceId, @event.ProductSourceId);
            Assert.Equal(expectedPrice, @event.NewPrice);
            Assert.Equal(0, @event.PreviousPrice);
            Assert.Equal(_testTime, @event.TimeStamp);
        }

        [Fact]
        public void when_updating_online_availability_then_add_new_product_online_availability()
        {
            const bool expectedAvailability = true;
            sut.When(new UpdateProductOnlineAvailability{IsAvailalbe = true, ProductId = ProductId, ProductSourceId = ProductSourceId});

            var @event = sut.ThenHasOne<OnlineAvailabilityUpdated>();
            Assert.Equal(ProductId, @event.SourceId);
            Assert.Equal(ProductSourceId, @event.ProductSourceId);
            Assert.Equal(expectedAvailability, @event.NewAvailability);
            Assert.Equal(false, @event.PreviousAvailability);
            Assert.Equal(_testTime, @event.TimeStamp);
        }
    }

    public class given_existing_product
    {
        private static readonly Guid ProductId = Guid.NewGuid();
        private const string BrandName = "TestBrandName";
        private static readonly Guid ProductSourceId = Guid.NewGuid();
        private static readonly string ProductName = "TestProductName";
        private const string ImageUrl = "http://TestProductImageUrl";
        private readonly Mock<IDateTimeService> _dateTimeSerive;
        private DateTime _testTime = DateTime.UtcNow;
        private EventSourcingTestHelper<Product> sut;
        private const Decimal initialPrice = 1.0M;

        public given_existing_product()
        {
            _dateTimeSerive = new Mock<IDateTimeService>();
            _dateTimeSerive.Setup(x => x.GetCurrentDateTimeUtc()).Returns(_testTime);
            sut = new EventSourcingTestHelper<Product>();
            sut.Setup(new ProductCommandHandler(sut.Repository, _dateTimeSerive.Object));

            sut.Given(
                new ProductCreated
                {
                    Brand = BrandName,
                    SourceId = ProductId,
                    Name = ProductName,
                    ImageUrl = ImageUrl
                },
                new PriceUpdated
                {
                    SourceId = ProductId,
                    NewPrice = initialPrice,
                    PreviousPrice = 0,
                    ProductSourceId = ProductSourceId,
                    TimeStamp = _testTime.AddHours(-1)
                },
                new OnlineAvailabilityUpdated
                {
                    SourceId = ProductId,
                    NewAvailability = true,
                    PreviousAvailability = false,
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

            var @event = sut.ThenHasOne<PriceUpdated>();
            Assert.Equal(ProductId, @event.SourceId);
            Assert.Equal(ProductSourceId, @event.ProductSourceId);
            Assert.Equal(expectedPrice, @event.NewPrice);
            Assert.Equal(initialPrice, @event.PreviousPrice);
            Assert.Equal(_testTime, @event.TimeStamp);
        }

        [Fact]
        public void when_Availability_from_same_source_changed_then_update_Availability()
        {
            const bool expectedAvailability = false;
            sut.When(new UpdateProductOnlineAvailability { IsAvailalbe = false, ProductId = ProductId, ProductSourceId = ProductSourceId });

            var @event = sut.ThenHasOne<OnlineAvailabilityUpdated>();
            Assert.Equal(ProductId, @event.SourceId);
            Assert.Equal(ProductSourceId, @event.ProductSourceId);
            Assert.Equal(expectedAvailability, @event.NewAvailability);
            Assert.Equal(true, @event.PreviousAvailability);
            Assert.Equal(_testTime, @event.TimeStamp);
        }
    }
}
