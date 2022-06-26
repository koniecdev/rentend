using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rentend.Models;
public class Car
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string Model { get; set; } = "";

    [Required]
    [Range(1970, 2024, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int YearOfProduction { get; set; }

    [Required]
    [MaxLength(6)]
    public string Engine { get; set; } = "";

    [Required]
    [Range(40, 1500, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int Horsepower { get; set; }

    [Required]
    [Range(0, 2, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int Drive { get; set; }

    [Required]
    [Range(0, 1, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int Transmission { get; set; }

    [Range(0, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int KmPerHalfDay { get; set; }
    [Range(0, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int KmPerDay { get; set; }
    [Range(0, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int PricePerHalfDay { get; set; }
    [Range(0, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int PricePerDay { get; set; }
    [Range(0, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int PricePerDayWeekend { get; set; }
    [Range(0, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int PricePerWeekend { get; set; }
    [Range(0, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int PricePerWeek { get; set; }
    [Range(0, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int PricePerMonth { get; set; }

    [Required]
    public int BrandId { get; set; }
    [ForeignKey("BrandId")]
    public virtual Brand Brand { get; set; }

    public int? DepartamentId { get; set; }
    [ForeignKey("DepartamentId")]
    public virtual Department Departament { get; set; }
}