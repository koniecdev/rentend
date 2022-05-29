using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rentend.Models;

public class Rent{
    [Key]
    public int Id { get; set; }
    public string UserId { get; set; }
    public int CarId { get; set; }
    
    [ForeignKey("CarId")]
    public virtual Car Car { get; set; }
    
    [ForeignKey("UserId")]
    public virtual IdentityUser IdentityUser { get; set; }
}