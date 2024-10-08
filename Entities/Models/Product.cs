using System.ComponentModel.DataAnnotations;

namespace Entities.Models;
public class Product
{
    [Key]
    public int ProductId { get; set; }
    public string? ProductName { get; set; } = String.Empty;
    public decimal Price { get; set; }
    public String? Summary { get; init; } = String.Empty;
    public String? ImageUrl { get; set; }
    public int? CategoryId { get; set; }
    public Category? Category { get; set; } // Reference navigation property
}
