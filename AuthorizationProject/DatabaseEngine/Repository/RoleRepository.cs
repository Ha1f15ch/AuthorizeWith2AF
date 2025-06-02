using AutoMapper;
using DatabaseEngine.DbModels;
using DatabaseEngine.RepositoryInterfaces;
using DTO.RoleModels;
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

		public async Task<Role> AddAsync(Role entity)
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

		public Task<Role> UpdateAsync(int id, Role entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<Role>> GetWithParameters() //параметризированный запрос 
		{
			throw new NotImplementedException();
		}
	}
}
