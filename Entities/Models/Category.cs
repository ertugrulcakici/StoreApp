using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public String? CategoryName { get; set; } = null!;
        public IEnumerable<Product>? Products { get; set; } // Collection navigation property
    }
}