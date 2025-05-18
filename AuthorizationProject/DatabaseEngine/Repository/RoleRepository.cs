using DatabaseEngine.DbModels;
using DatabaseEngine.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DatabaseEngine.Repository
{
	public class RoleRepository : IRoleRepository
	{
		private readonly AppDbContext _context;

		public RoleRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<Role?> CreateNewRole(string roleCode, string description)
		{
			if(string.IsNullOrEmpty(roleCode)) return null;

			if (string.IsNullOrEmpty(description)) return null;

			Console.WriteLine($"Создание новой записи Role");
			Console.WriteLine($"Поиск существующей роли по коду = {roleCode}");

			var existedRole = await GetRoleByCode( roleCode );

			if(existedRole is null)
			{
				Console.WriteLine($"Выполнить операцию создания роли не получилось, в БД уже есть такая запись, возвращен null");
				return existedRole;
			}
			else
			{
				Console.WriteLine($"В БД записи с данным кодом не обнаружено, создаем новую роль");

				var newRole = new Role
				{
					RoleCode = roleCode,
					Description = description
				};

				await _context.Roles.AddAsync( newRole );
				await _context.SaveChangesAsync();

				Console.WriteLine($"Роль с кодом = {roleCode} успешно создана");

				return newRole;
			}
		}

		public async Task<bool> DeleteRole(string roleCode)
		{
			if (string.IsNullOrEmpty(roleCode)) return false;

			Console.WriteLine($"Поиск существующей роли по коду = {roleCode}");

			var existedRole = await GetRoleByCode(roleCode);

			if(existedRole != null)
			{
				Console.WriteLine($"Роль с кодом {roleCode} найдена, выполняем удаление");

				_context.Roles.Remove(existedRole);
				await _context.SaveChangesAsync();
				
				Console.WriteLine($"Роль с кодом {roleCode} успешно удалена");

				return true;
			}
			else
			{
				Console.WriteLine($"Найти роль с заданным кодом = {roleCode} не удалось, выполнение операции завершено");
				return false;
			}
		}

		public async Task<List<Role>> GetAllRoles()
		{
			Console.WriteLine($"К выводу {_context.Roles.Count()} записей из таблицы Role");
			return await _context.Roles.ToListAsync();
		}

		public async Task<Role?> GetRoleByCode(string roleCode)
		{
			Console.WriteLine($"Поиск роли по коду - {roleCode}");
			return await _context.Roles.FirstOrDefaultAsync(el => el.RoleCode == roleCode);
		}
	}
}
