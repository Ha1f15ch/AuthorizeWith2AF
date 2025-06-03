using DatabaseEngine.DbModels;

namespace DatabaseEngine.RepositoryInterfaces
{
	public interface IUserRepository
	{
		public Task<User?> CreateNewUser(User user);
		public Task<User?> GetUserById(int userId);
		public Task<User?> GetUserByName(string userName);
		public Task<User?> GetUserByEmail(string userEmail);
		public Task<bool> ApproveUser(int userId);
		public Task<bool> DeleteUser(int userId);
		public Task<User?> UpdateUser(int userId, User updateUserEntity);
		public string? GeneratePassword(string innerString);
		public Task<List<User>> GetAllUsers();
		public Task<List<User>> GetAllActiveAndNotDeletedUsers();
		public Task<List<User>> GetAllNonActiveAndNotDeletedUsers();
		public Task<List<User>> GetAllDeletedUsers();
	}
}
