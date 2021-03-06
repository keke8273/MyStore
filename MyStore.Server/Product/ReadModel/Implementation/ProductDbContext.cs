﻿using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Store.ReadModel.Implementation
{
    public class ProductDbContext : DbContext
    {
        public const string SchemaName = "StoreManagement";

        public ProductDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().ToTable("ProductView", SchemaName);
            modelBuilder.Entity<Category>().ToTable("Categories", SchemaName);
            modelBuilder.Entity<ProductSource>().ToTable("ProductSource", SchemaName);

            modelBuilder.Entity<Product>().HasMany<Category>(p => p.Categories)
                .WithMany(c => c.Products).Map(
                    pc =>
                    {
                        pc.MapLeftKey("ProductRefId");
                        pc.MapRightKey("CategoryRefId");
                        pc.ToTable("ProductCategory", SchemaName);
                    });           

            //Conventions
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories{ get; set; }
        public DbSet<ProductSource> Sources { get; set; }

        public T Find<T>(Guid id) where T : class
        {
            return Set<T>().Find(id);
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return this.Set<T>();
        }

        public void Save<T>(T entity) where T : class
        {
            var entry = this.Entry(entity);

            if (entry.State == EntityState.Detached)
                this.Set<T>().Add(entity);

            SaveChanges();
        }
    }
}
