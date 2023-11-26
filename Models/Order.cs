namespace FakeDealerAPI.Models;

using System.ComponentModel.DataAnnotations;

public class Order 
{
    [Key]
    public int Id { get; set;  }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Lastname { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? PaymentMethod { get; set; }

    [Required]
    [Phone]
    public string? PhoneNumber { get; set; }

    [Required]
    public DateTime Appointment { get; set; }

    [Required]
    public Car? Car { get; set; }

}