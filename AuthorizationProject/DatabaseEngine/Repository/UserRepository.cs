using DatabaseEngine.DbModels;
using DatabaseEngine.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;

namespace DatabaseEngine.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly string _secretKeyToPwd;
		private readonly AppDbContext _context;

		public UserRepository(AppDbContext context, IConfiguration configuration)
		{
			_context = context;
			_secretKeyToPwd = configuration["SecretKeyForUserPwd"];
		}


		public Task<User?> CreateNewUser(User user)
		{
			throw new NotImplementedException();
		}

		public Task<User?> GetUserById(int userId)
		{
			throw new NotImplementedException();
		}

		public Task<User?> GetUserByName(string userName)
		{
			throw new NotImplementedException();
		}

		public Task<User?> GetUserByEmail(string userEmail)
		{
			throw new NotImplementedException();
		}

		public Task<bool> ApproveUser(int userId)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteUser(int userId)
		{
			throw new NotImplementedException();
		}

		public Task<User?> UpdateUser(int userId, User updateUserEntity)
		{
			throw new NotImplementedException();
		}

		public string? GeneratePassword(string innerString)
		{
			throw new NotImplementedException();
		}

		public Task<List<User>> GetAllUsers()
		{
			throw new NotImplementedException();
		}

		public Task<List<User>> GetAllActiveAndNotDeletedUsers()
		{
			throw new NotImplementedException();
		}

		public Task<List<User>> GetAllNonActiveAndNotDeletedUsers()
		{
			throw new NotImplementedException();
		}

		public Task<List<User>> GetAllDeletedUsers()
		{
			throw new NotImplementedException();
		}
	}
}
