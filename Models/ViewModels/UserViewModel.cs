using Microsoft.AspNetCore.Identity;

namespace rentend.Models;

public class UserViewModel
{
    public string Email {get;set;} = "";
    public string Password {get;set;} = "";
    public bool Remember {get;set;} = false;
}