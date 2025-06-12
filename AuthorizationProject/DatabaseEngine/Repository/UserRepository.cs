using DatabaseEngine.DbModels;
using DatabaseEngine.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
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


		public async Task<User?> CreateNewUser(User user)
		{
			var existedUserName = await _context.Users.AnyAsync(el => el.UserName == user.UserName);
			
			if (existedUserName)
			{
				Console.WriteLine($"Пользователь с таким UserName уже есть");
				return null;
			}
			
			var existedUserEmail = await _context.Users.AnyAsync(el => el.UserEmail == user.UserEmail);

			if (existedUserEmail)
			{
				Console.WriteLine($"Пользователь с таким UserEmail уже есть");
				return null;
			}

			Console.WriteLine($"Создаем пользователя");
			var newUser = new User
			{
				UserName = user.UserName,
				UserEmail = user.UserEmail,
				UserPassword = user.UserPassword,
				CreatedDate = DateTime.Now,
				DeleteDate = null,
				IsActive = true,
				IsApprove = false,
			};
			
			await _context.Users.AddAsync(newUser);
			await _context.SaveChangesAsync();
			Console.WriteLine($"Пользователь успешно зарегистрирован. {newUser.UserName} - {newUser.Id}");
			
			return newUser;
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
