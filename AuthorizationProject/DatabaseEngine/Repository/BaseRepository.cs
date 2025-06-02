using DatabaseEngine.RepositoryInterfaces;

namespace DatabaseEngine.Repository
{
	public abstract class BaseRepository<TReceive, TResponse> : IBaseInterface<TReceive, TResponse> 
		where TReceive : class
		where TResponse : class
	{
		private readonly AppDbContext _dbContext;
		
		public BaseRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		
		public virtual Task<TResponse> AddAsync(TReceive entity)
		{
			throw new NotImplementedException("Метод AddAsync не реализован в данном репозитории");
		}

		public virtual Task<TResponse> DeleteAsync(int id)
		{
			throw new NotImplementedException("Метод DeleteAsync не реализован в данном репозитории");
		}

		public virtual Task<List<TResponse>> GetAllAsync()
		{
			throw new NotImplementedException("Метод GetAllAsync не реализован в данном репозитории");
		}

		public virtual Task<TResponse> GetByIdAsync(int id)
		{
			throw new NotImplementedException("Метод GetByIdAsync не реализован в данном репозитории");
		}

		public virtual Task<TResponse> GetWithParameters(TReceive parameterizedEntity)
		{
			throw new NotImplementedException("Метод GetWithParameters не реализован в данном репозитории");
		}

		public virtual Task<TResponse> UpdateAsync(int id, TReceive entity)
		{
			throw new NotImplementedException("Метод UpdateAsync не реализован в данном репозитории");
		}
	}
}
