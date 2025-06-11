using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseEngine.DbModels
{
	[Table("RefreshToken", Schema = "dbo")]
	public class RefreshToken
	{
		public RefreshToken()
		{
			
		}

		[Key]
		public int Id { get; set; } // Уникальный Id токена 
		public int UserId { get; set; }
		public string UniqueRefreshToken { get; set; } // Значение токена
		public DateTime DateCreated { get; set; }
		public DateTime DateExpired { get; set; } // Дата истечения токена

		public User User { get; set; }
	}
}
