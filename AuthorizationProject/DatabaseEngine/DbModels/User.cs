using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseEngine.DbModels
{
	[Table("User", Schema = "dbo")]
	public class User
	{
		public User()
		{
			
		}

		[Key]
		public int Id { get; set; }
		public string UserName { get; set; }
		public string UserEmail { get; set; }
		public string UserPassword { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? DeleteDate { get; set; }
		public bool IsActive { get; set; }
		public bool IsApprove { get; set; } // Созданный пользователь подтвержден ?

		public RefreshToken RefreshToken { get; set; }
		public List<UserRole> UserRoles { get; set; }
	}
}
