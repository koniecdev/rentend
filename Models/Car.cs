using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rentend.Models;

public class Car{
    [Key]
    public int Id {get;set;}
    [Required]
    public string Model {get;set;} = "";

    [Required]
    public int YearOfProduction { get; set; }

    [Required]
    public double Engine { get; set; }

    [Required]
    public int Horsepower { get; set; }

    [Required]
    public string Drive { get; set; } = "";

    [Required]
    public string Transmission { get; set; } = "";

    public int KmPerHalfDay { get; set; }

    public int KmPerDay { get; set; }

    public int PricePerHalfDay { get; set; }
    public int PricePerDay { get; set; }
    public int PricePerDayWeekend { get; set; }
    public int PricePerWeekend { get; set; }
    public int PricePerWeek { get; set; }
    public int PricePerMonth { get; set; }

    [Required]
    public int BrandId {get;set;}
    [ForeignKey("BrandId")]
    public virtual Brand Brand {get;set;}
}