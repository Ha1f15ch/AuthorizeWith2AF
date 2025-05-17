using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEngine.DbModels
{
	[Table("UserRole", Schema = "dbo")]
	public class UserRole
	{
		public UserRole()
		{
			
		}

		[Key]
		public int Id { get; set; }
		public int UserId { get; set; }
		public int RoleId { get; set; }

		public User User { get; set; }
		public Role Role { get; set; }
	}
}
