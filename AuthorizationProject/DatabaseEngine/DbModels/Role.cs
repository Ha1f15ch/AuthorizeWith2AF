using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEngine.DbModels
{
	[Table("Role", Schema = "dict")]
	public class Role
	{
		public Role()
		{
			
		}

		[Key]
		public string RoleCode { get; set; }
		public string Description { get; set; }

		public List<UserRole> UserRoles { get; set; }
	}
}
