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
		
		public async Task<Role?> GetByCodeAsync(string roleCode)
		{
			var role = await _context.Roles.FindAsync(roleCode);
			return role;
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
				await _context.Roles.AddAsync(entity);
				await _context.SaveChangesAsync();
				return entity;
			}

			Console.WriteLine("Передано некорректное значение, запись с таким кодом уже существует");
			return existedRole;
		}

		public async Task<Role?> UpdateAsync(string code, Role entity)
		{
			var roleForUpdate = await _context.Roles.FindAsync(code);

			if (roleForUpdate != null)
			{
				var existedRoleByRoleCode = await GetByCodeAsync(entity.RoleCode);
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

			Console.WriteLine($"При выполнении операции обновления возникла ошибка. Роль по id = {code} не была найдена");
			return null;
		}

		public async Task<bool> DeleteAsync(string code)
		{
			var roleForDelete = await _context.Roles.FindAsync(code);

			if (roleForDelete != null)
			{
				_context.Roles.RemoveRange(roleForDelete);
				await _context.SaveChangesAsync();

				Console.WriteLine($"Успешно удалено");
				return true;
			}
			
			Console.WriteLine($"Удалить не полуилось, не найдена роль по коду - {code}");
			return false;
		}

		public Task<List<Role>> GetWithParameters() //параметризированный запрос 
		{
			throw new NotImplementedException("Метод GetWithParameters не реализован");
		}
	}
}
