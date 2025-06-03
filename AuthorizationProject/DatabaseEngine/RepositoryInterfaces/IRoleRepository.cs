using DatabaseEngine.DbModels;

namespace DatabaseEngine.RepositoryInterfaces
{
	public interface IRoleRepository
	{
		public Task<Role?> GetByCodeAsync(string roleCode);
		public Task<List<Role>> GetAllAsync();
		public Task<Role?> AddAsync(Role entity);
		public Task<Role?> UpdateAsync(string code, Role entity);
		public Task<bool> DeleteAsync(string code);
		public Task<List<Role>> GetWithParameters();
	}
}
