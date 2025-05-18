using DatabaseEngine.DbModels;

namespace DatabaseEngine.InitializeData
{
	public static class RoleDataInitialize
	{
		public static async Task Initialize(AppDbContext context)
		{
			if(!context.Roles.Any())
			{
				Console.WriteLine("Подготовили данные для внесения в справочник п оумолчанию");

				var defaultRoles = new List<Role>
				{
					new Role {RoleCode = "USER", Description = "Стандартная роль пользователя ресурса"},
					new Role {RoleCode = "ADMIN", Description = "Роль с неограниченными правами"},
					new Role {RoleCode = "MODIFY", Description = "Роль с допуском изменять справочники"},
					new Role {RoleCode = "DELETE", Description = "РОль с допуском удалять записи в справочниках"}
				};

				await context.Roles.AddRangeAsync(defaultRoles);
				await context.SaveChangesAsync();

				Console.WriteLine("Данные внесены и сохранены в БД успешно");
			}
			else
			{
				Console.WriteLine("В базе данных записи уже существуют, пропускаем операцию");
			}
		}
	}
}
