using System;
using System.Collections.Generic;
using System.IO;
using MyStore.PriceTracker.Client.Services.Implementation;
using Store.Dto;
using Xunit;

namespace MyStore.PriceTracker.ClientTests.Services.Implementation
{
    public class given_correct_path
    {
        private static readonly string singleEntryPath = Directory.GetCurrentDirectory() + @"\TestData\single_entry.csv";
        private static readonly string singleEntryPathWithoutCategory = Directory.GetCurrentDirectory() + @"\TestData\single_entry_no_category.csv";
        private CsvProductReader sut;

        public given_correct_path()
        {
        }

        [Fact()]
        public void when_read_single_entry()
        {
            sut = new CsvProductReader(singleEntryPath);

            var expected = new List<SourceBasedProductDto>
            {
                new SourceBasedProductDto
                {
                    Brand = "Wagner",
                    Categories = new[]{"Bone and Joint Health"},
                    ImageUrl = @"https://static.chemistwarehouse.com.au/ams/media/productimages/64412/200.jpg",
                    IsAvailableOnline = true,
                    Name = "Wagner Total Calcium Complete 150 Tablets",
                    Price = 15.24M,
                    RetailPrice = 30.49M,
                }
            };

            var products = sut.GetProductInfo();

            Assert.Equal(expected, products);
        }

        [Fact()]
        public void when_read_single_entry_with_no_category()
        {
            sut = new CsvProductReader(singleEntryPathWithoutCategory);

            var expected = new List<SourceBasedProductDto>
            {
                new SourceBasedProductDto
                {
                    Brand = "Wagner",
                    Categories = new String[]{},
                    ImageUrl = @"https://static.chemistwarehouse.com.au/ams/media/productimages/64412/200.jpg",
                    IsAvailableOnline = true,
                    Name = "Wagner Total Calcium Complete 150 Tablets",
                    Price = 15.24M,
                    RetailPrice = 30.49M
                }
            };

            var products = sut.GetProductInfo();

            Assert.Equal(expected, products);
        }
    }
}
