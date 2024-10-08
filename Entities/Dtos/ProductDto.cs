using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record ProductDto
    {
        public int ProductId { get; init; }
        [Required(ErrorMessage = "Product name is required")]
        public string? ProductName { get; init; } = String.Empty;
        [Required(ErrorMessage = "Product description is required")]
        public decimal Price { get; init; }
        public String? Summary { get; set; } = String.Empty;
        public String? ImageUrl { get; set; }
        public int? CategoryId { get; init; }
    }
}