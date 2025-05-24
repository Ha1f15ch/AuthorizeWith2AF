namespace DatabaseEngine.RepositoryInterfaces
{
	public interface IUserRoleRepository<TReceive, TResponse> : IBaseInterface<TReceive, TResponse>
		where TReceive : class
		where TResponse : class
	{
		
	}
}
