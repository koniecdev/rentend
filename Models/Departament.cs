using System.ComponentModel.DataAnnotations;

namespace rentend.Models;

public class Departament{
    [Key]
    public int Id { get; set; }
    [Required]
    public string City { get; set; } = "";
    public string FullAddress { get; set; } = "";
}