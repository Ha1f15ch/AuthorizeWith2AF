using AutoMapper;
using DatabaseEngine.DbModels;
using DatabaseEngine.RepositoryInterfaces;

namespace DatabaseEngine.Repository
{
	public class RoleRepository<TReceive, TResponse>
		: IRoleRepository<TReceive, TResponse>
		where TReceive : class
		where TResponse : class
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		
		public RoleRepository(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		
		public async Task<TResponse?> GetByCodeAsync(string roleCode)
		{
			var role = await _context.Roles.FindAsync(roleCode);
			return _mapper.Map<TResponse>(role);
		}

		public Task<TResponse> GetByIdAsync(int id)
		{
			throw new NotImplementedException("Данный формат данных не поддерживается");
		}

		public Task<List<TResponse>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<TResponse> AddAsync(TReceive entity)
		{
			var roleModel = _mapper.Map<Role>(entity);
			var existedRole = await GetByCodeAsync(roleModel.RoleCode);

			if (existedRole == null)
			{
				await _context.Roles.AddAsync(roleModel);
				await _context.SaveChangesAsync();
				return _mapper.Map<TResponse>(roleModel);
			}

			Console.WriteLine("Передано некорректное значение, запись с таким кодом уже существует");
			return _mapper.Map<TResponse>(existedRole);
		}

		public Task<TResponse> UpdateAsync(int id, TReceive entity)
		{
			throw new NotImplementedException();
		}

		public Task<TResponse> DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<TResponse> GetWithParameters(TReceive parameterizedEntity)
		{
			throw new NotImplementedException();
		}
	}
}
