using System.ComponentModel.DataAnnotations;

namespace rentend.Models;
public class Brand
{
    [Key]
    public int Id { get; set; }

    [Required]
	[MaxLength(100)]
    public string Name { get; set; } = "";

	public Brand(){}
	public Brand(int id, string name)
	{
		(Id, Name) = (id, name);
	}
}
