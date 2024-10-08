using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryId);
            builder.Property(c => c.CategoryName).IsRequired().HasMaxLength(60);
            builder.HasData(
                new Category { CategoryId = 1, CategoryName = "Fruit" },
                new Category { CategoryId = 2, CategoryName = "Vegetable" },
                new Category { CategoryId = 3, CategoryName = "Meat" },
                new Category { CategoryId = 4, CategoryName = "Book" }
            );
        }
    }
}