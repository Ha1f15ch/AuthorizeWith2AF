namespace DatabaseEngine.RepositoryInterfaces
{
	public interface IBaseInterface<TReceive, TResponse> where TReceive : class
	{
		Task<TResponse> GetByIdAsync(int id);
		Task<List<TResponse>> GetAllAsync();
		Task<TResponse> AddAsync(TReceive entity);	
		Task<TResponse> UpdateAsync(int id, TReceive entity);
		Task<TResponse> DeleteAsync(int id);
		Task<TResponse> GetWithParameters(TReceive parameterizedEntity);
	}
}
