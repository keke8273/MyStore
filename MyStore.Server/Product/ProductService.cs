using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CQRS.Infrastructure.Messaging;
using CQRS.Infrastructure.Utils;
using Store.Contracts;
using Store.ReadModel;
using Store.ReadModel.Implementation;

namespace Store
{
    public class ProductService
    {
        private readonly IEventBus eventBus;
        private readonly string nameOrConnectionString;

        public ProductService(IEventBus eventBus, string nameOrConnectionString ="StoreManagement")
        {
            this.eventBus = eventBus;
            this.nameOrConnectionString = nameOrConnectionString;
        }

        public Guid CreateProduct(ProductInfo productInfo)
        {
            using (var context = new ProductDbContext(nameOrConnectionString))
            {
                var existingProduct = context.Products.Any(p => p.Name == productInfo.Name);

                if(existingProduct)
                    throw new DuplicateNameException("The productInfo already exists");

                var product = new ReadModel.Product(productInfo.Id, productInfo.Brand, productInfo.Name,
                    productInfo.ImageUrl);

                foreach (var categoryName in productInfo.Categories)
                {
                    var category = context.Categories.FirstOrDefault(c => c.Name == categoryName) ??
                                   new Category(categoryName);

                    product.Categories.Add(category);
                }

                context.Products.Add(product);

                context.SaveChanges();

                eventBus.Publish(new ProductCreated
                {
                    SourceId = productInfo.Id,
                    Brand = productInfo.Brand,
                    Name = productInfo.Name,
                    ImageUrl = productInfo.ImageUrl
                });

                return productInfo.Id;
            }
        }

        public Source FindSource(string sourceName)
        {
            using (var context = new ProductDbContext(nameOrConnectionString))
            {
                return context.Sources.FirstOrDefault(s => s.Name == sourceName);
            }
        }

        public Source CreateSource(string sourceName)
        {
            using (var context = new ProductDbContext(nameOrConnectionString))
            {
                var source = new Source {Id = GuidUtil.NewSequentialId(), Name = sourceName};

                context.Sources.Add(source);
                context.SaveChanges();
                return source;
            }
        }
    }

    public class ProductInfo
    {
        public ProductInfo()
        {
            Id = GuidUtil.NewSequentialId();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
}
