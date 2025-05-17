using DatabaseEngine.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DatabaseEngine
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<RefreshToken> RefreshTokens { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }
	}
}
