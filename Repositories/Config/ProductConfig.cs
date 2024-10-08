using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.ProductName).IsRequired().HasMaxLength(60);
            builder.Property(p => p.Price).IsRequired();
            builder.HasData(
                new Product { ProductId = 1, CategoryId = 1, ImageUrl = "/images/1.jpg", ProductName = "Apple", Price = 1000m },
                new Product { ProductId = 2, CategoryId = 2, ImageUrl = "/images/2.jpg", ProductName = "Banana", Price = 200 },
                new Product { ProductId = 3, CategoryId = 3, ImageUrl = "/images/3.jpg", ProductName = "Cherry", Price = 1_600 },
                new Product { ProductId = 4, CategoryId = 2, ImageUrl = "/images/4.jpg", ProductName = "Durian", Price = 2_000 },
                new Product { ProductId = 5, CategoryId = 1, ImageUrl = "/images/5.jpg", ProductName = "Eggplant", Price = 500 },
                new Product { ProductId = 6, CategoryId = 4, ImageUrl = "/images/6.jpg", ProductName = "Fennel", Price = 1_000 }
            );
        }
    }
}