using DatabaseEngine.DbModels;

namespace DatabaseEngine.RepositoryInterfaces
{
	public interface IRoleRepository : IBaseInterface
	{
		public Task<Role?> CreateNewRole(string roleCode, string description);
		public Task<Role?> GetRoleByCode(string roleCode);
		public Task<List<Role>> GetAllRoles();
		public Task<bool> DeleteRole(string roleCode);
	}
}
