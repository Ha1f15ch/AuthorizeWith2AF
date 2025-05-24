using DatabaseEngine.RepositoryInterfaces;

namespace DatabaseEngine.Repository
{
	public abstract class BaseRepository<TReceive, TResponse> : IBaseInterface<TReceive, TResponse> 
		where TReceive : class
		where TResponse : class
	{
		public virtual Task<TResponse> AddAsync(TReceive entity)
		{
			throw new NotImplementedException();
		}

		public virtual Task<TResponse> DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public virtual Task<List<TResponse>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public virtual Task<TResponse> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public virtual Task<TResponse> GetWithParameters(TReceive parameterizedEntity)
		{
			throw new NotImplementedException();
		}

		public virtual Task<TResponse> UpdateAsync(int id, TReceive entity)
		{
			throw new NotImplementedException();
		}
	}
}
