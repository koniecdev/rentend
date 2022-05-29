using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rentend.Models;

public class Pin{
    [Key]
    public int Id { get; set; }
    public int CarId { get; set; }
    
    [ForeignKey("CarId")]
    public virtual Car Car { get; set; }
}