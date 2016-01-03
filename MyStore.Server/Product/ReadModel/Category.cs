using System;
using System.Collections.Generic;
using CQRS.Infrastructure.Utils;

namespace Store.ReadModel
{
    public class Category
    {
        protected Category()
        {
            Id = GuidUtil.NewSequentialId();
        }

        public Category(string name) : this()
        {
            Name = name;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Product> Products{ get; set; }
    }
}