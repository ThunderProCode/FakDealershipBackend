namespace FakeDealerAPI.Models;

using System.ComponentModel.DataAnnotations;


public class Car
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Make { get; set; }
    [Required]
    public string? Model { get; set; }
    [Required]
    public int? Year { get; set; }
    [Required]
    public int? Mileage { get; set; }
    [Required]
    public int? Mpg { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public string? VIN { get; set; }
    public List<string>? Images { get; set; }
    [Required]
    public DateTime Created { get; set; }
}