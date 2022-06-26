namespace rentend.Models;
public class ApplicationUser : IdentityUser
{
	public string Address { get; set; } = "";

	public ApplicationUser(){}

	public ApplicationUser(string address)
	{
		Address = address;
	}
}