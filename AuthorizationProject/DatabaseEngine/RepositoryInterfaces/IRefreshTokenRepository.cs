namespace DatabaseEngine.RepositoryInterfaces
{
	public interface IRefreshTokenRepository<TReceive, TResponse> : IBaseInterface<TReceive, TResponse>
		where TReceive : class
		where TResponse : class
	{

	}
}
