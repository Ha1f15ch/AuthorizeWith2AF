using DatabaseEngine.DbModels;

namespace DatabaseEngine.RepositoryInterfaces
{
	public interface IUserRepository : IBaseInterface
	{
		public Task<User?> CreateNewUser(string userName, string userEmail, string userPassword);
		public Task<User?> GetUserById(int userId);
		public Task<User?> GetUserByName(string userName);
		public Task<User?> GetUserByEmail(string userEmail);
		public Task<bool> ApproveUser(int userId);
		public Task<bool> DeleteUser(int userId);
		public Task<User?> UpdateUser(int userId, string newUserName, string newUserEmail);
		public string? GeneratePassword(string innerString);
		public Task<List<User>> GetAllUsers();
		public Task<List<User>> GetAllActiveAndNotDeletedUsers();
		public Task<List<User>> GetAllNonActiveAndNotDeletedUsers();
		public Task<List<User>> GetAllDeletedUsers();
	}
}
