using AutoMapper;
using DatabaseEngine.DbModels;
using DatabaseEngine.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DatabaseEngine.Repository
{
	public class RoleRepository
		: IRoleRepository
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		
		public RoleRepository(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		
		public async Task<Role?> GetByRoleIdAsync(int roleId)
		{
			var role = await _context.Roles.FindAsync(roleId);
			return role;
		}

		public async Task<Role?> GetByCodeAsync(string roleCode)
		{
			var role = await _context.Roles.FirstOrDefaultAsync(val => val.RoleCode == roleCode);
			
			return role ?? role;
		}
		
		public async Task<List<Role>> GetAllAsync()
		{
			var roles = await _context.Roles.ToListAsync();
			return roles;
		}

		public async Task<Role?> AddAsync(Role entity)
		{
			var existedRole = await GetByCodeAsync(entity.RoleCode);

			if (existedRole == null)
			{
				entity.CreatedDate = DateTime.Now;
				
				await _context.Roles.AddAsync(entity);
				await _context.SaveChangesAsync();
				return entity;
			}

			Console.WriteLine("Передано некорректное значение, запись с таким кодом уже существует");
			return existedRole;
		}

		public async Task<Role?> UpdateAsync(int roleId, Role entity)
		{
			var roleForUpdate = await _context.Roles.FindAsync(roleId); // По id

			if (roleForUpdate != null)
			{
				var existedRoleByRoleCode = await GetByCodeAsync(entity.RoleCode); // Прооверяем введенные данные п окоду
				// Случай, если меняется все, кроме RoleCode
				if ((existedRoleByRoleCode != null) && (existedRoleByRoleCode.RoleCode == roleForUpdate.RoleCode))
				{
					Console.WriteLine($"Изменяется роль без замены значения кода роли");
					roleForUpdate.Description = entity.Description;
					await _context.SaveChangesAsync();

					return roleForUpdate;
				}
				// Случай, когда меняется все
				if (existedRoleByRoleCode == null)
				{
					Console.WriteLine($"Изменяется роль с заменой значения кода роли");
					
					roleForUpdate.RoleCode = entity.RoleCode;
					roleForUpdate.Description = entity.Description;
					await _context.SaveChangesAsync();

					return roleForUpdate;
				}
				// Если код уже занят другой ролью
				Console.WriteLine("Указанный код принадлежит другой роли");
				return existedRoleByRoleCode;
			}

			Console.WriteLine($"При выполнении операции обновления возникла ошибка. Роль по id = {roleId} не была найдена");
			return null;
		}

		public async Task<bool> DeleteAsync(int roleId)
		{
			var roleForDelete = await GetByRoleIdAsync(roleId);

			if (roleForDelete != null)
			{
				roleForDelete.DeletedDate = DateTime.Now;
				await _context.SaveChangesAsync();

				Console.WriteLine($"Успешно удалено");
				return true;
			}
			
			Console.WriteLine($"Удалить не полуилось, не найдена роль по id - {roleId}");
			return false;
		}

		public Task<List<Role>> GetWithParameters() //параметризированный запрос 
		{
			throw new NotImplementedException("Метод GetWithParameters не реализован");
		}
	}
}
