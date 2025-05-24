using DTO.RoleModels;

namespace DatabaseEngine.RepositoryInterfaces
{
	public interface IRoleRepository<TReceive, TResponse> : IBaseInterface<TReceive, TResponse> 
		where TReceive : class 
		where TResponse : class
	{
		public Task<TResponse?> GetByCodeAsync(string roleCode);
	}
}
