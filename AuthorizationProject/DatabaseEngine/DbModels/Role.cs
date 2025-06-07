using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseEngine.DbModels
{
	[Table("Role", Schema = "dict")]
	public class Role
	{
		public Role()
		{
			
		}

		[Key]
		public int Id { get; set; }
		public string RoleCode { get; set; }
		public string? Description { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }

		public List<UserRole> UserRoles { get; set; }
	}
}
