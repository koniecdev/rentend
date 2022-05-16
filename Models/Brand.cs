using System.ComponentModel.DataAnnotations;

namespace rentend.Models;

public class Brand{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = "";
}