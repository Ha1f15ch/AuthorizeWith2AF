using DatabaseEngine.DbModels;
using DTO.RoleModels;

namespace DatabaseEngine.RepositoryInterfaces
{
	public interface IRoleRepository
	{
		public Task<Role?> GetByCodeAsync(string roleCode);
	}
}
