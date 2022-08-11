using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain;


namespace Database
{
	public class MyDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
	{
		public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
		{
		}

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Account> Account { get; set; }
		public DbSet<Transaction> Transaction { get; set; }
		public DbSet<TransactionType> TransactionType { get; set; }
		public DbSet<AccountType> Type { get; set; }

	}
}