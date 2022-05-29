using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace rentend.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Brand> Brands {get;set;}
		public DbSet<Car> Cars {get;set;}
		public DbSet<Pin> Pins {get;set;}
		public DbSet<Rent> Rents {get;set;}
	}
}