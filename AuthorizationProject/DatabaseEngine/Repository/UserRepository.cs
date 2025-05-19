using BusinessEngine.Services.Interfaces;
using DatabaseEngine.DbModels;
using DatabaseEngine.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DatabaseEngine.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly string _secretKeyToPwd;
		private readonly IGeneratePassword _generatePasswordService;
		private readonly AppDbContext _context;

		public UserRepository(AppDbContext context, IConfiguration configuration, IGeneratePassword generatePassword)
		{
			_context = context;
			_secretKeyToPwd = configuration["SecretKeyForUserPwd"];
			_generatePasswordService = generatePassword;
		}

		public async Task<bool> ApproveUser(int userId)
		{
			if(userId <=0 )
			{
				Console.WriteLine($"Передан некорректный идентификатор");
				return false;
			}

			Console.WriteLine($"Поиск пользователя по id = {userId}");

			var user = await _context.Users.FindAsync( userId );
			
			if( user == null )
			{
				Console.WriteLine($"Пользователь с таким id = {userId} не найден");
				return false;
			}

			Console.WriteLine($"Пользователь найден, выполняется подтверждение");

			user.IsApprove = true;

			await _context.SaveChangesAsync();

			Console.WriteLine($"Пользователь с id = {userId} подтвержден");

			return true;
		}

		public async Task<User?> CreateNewUser(string userName, string userEmail, string userPassword)
		{
			if(string.IsNullOrEmpty(userName)) return null;
			if(string.IsNullOrEmpty(userEmail)) return null;
			if(string.IsNullOrEmpty(userPassword)) return null;

			Console.WriteLine($"Поиск пользователей с уже заданными userName = {userName} и userEmail = {userEmail}");

			var receiveUserByName = await GetUserByName(userName);
			var receiveUserByEmail = await GetUserByEmail(userEmail);

			if(receiveUserByName != null)
			{
				Console.WriteLine($"Найден пользователь с указанным параметром name = {userName}. Id пользователя - {receiveUserByName.Id}");
				return null;
			}

			if(receiveUserByEmail != null)
			{
				Console.WriteLine($"Найден пользователь с указанным параметром email = {userEmail}. Id пользователя - {receiveUserByEmail.Id}");
				return null;
			}

			var encodedPassword = _generatePasswordService.GeneratePassword(userPassword);

			if(encodedPassword == null)
			{
				Console.WriteLine($"При хэшировании пароля возникла ошибка");
				return null;
			}

			Console.WriteLine($"Выполняем создание записи пользователя");

			var newUser = new User
			{
				UserName = userName,
				UserEmail = userEmail,
				UserPassword = encodedPassword,
				CreatedDate = DateTime.Now,
				DeleteDate = null,
				IsActive = true,
				IsApprove = false,
			};

			await _context.Users.AddAsync(newUser);
			await _context.SaveChangesAsync();

			Console.WriteLine($"Создание пользователя выполнено успешно. Присвоен id = {newUser.Id}");

			return newUser;
		}

		public async Task<bool> DeleteUser(int userId)
		{
			if (userId <= 0) return false;

			Console.WriteLine($"Поиск пользователя с id = {userId}");

			var user = await _context.Users.FindAsync(userId);

			if (user == null)
			{
				Console.WriteLine($"Пользователь с таким id = {userId} не найден");
				return false;
			}

			Console.WriteLine($"Пользователь найден, выполняется удаление");

			user.DeleteDate = DateTime.Now;

			await _context.SaveChangesAsync();

			Console.WriteLine($"Пользователь успешно удален");

			return true;
		}

		public async Task<List<User>> GetAllUsers()
		{
			var users = await _context.Users.ToListAsync();

			Console.WriteLine($"Будет возвращено - {users.Count} записей всех пользователей");

			return users;
		}

		public async Task<List<User>> GetAllActiveAndNotDeletedUsers()
		{
			var users = await _context.Users.Where(x => x.IsActive && x.DeleteDate == null).ToListAsync();

			Console.WriteLine($"Будет возвращено - {users.Count} записей активных неудаленных пользователей");

			return users;
		}

		public async Task<List<User>> GetAllNonActiveAndNotDeletedUsers()
		{
			var users = await _context.Users.Where(x => !x.IsActive && x.DeleteDate == null).ToListAsync();

			Console.WriteLine($"Будет возвращено - {users.Count} записей неактивных и неудаленных пользователей");

			return users;
		}

		public async Task<List<User>> GetAllDeletedUsers()
		{
			var users = await _context.Users.Where(x => x.DeleteDate != null).ToListAsync();

			Console.WriteLine($"Будет возвращено - {users.Count} удаленных записей пользователей");

			return users;
		}

		public async Task<User?> GetUserByEmail(string userEmail)
		{
			if (string.IsNullOrEmpty(userEmail)) return null;

			Console.WriteLine($"Поиск пользователей с email = {userEmail}");

			var existedUser = await _context.Users.FirstOrDefaultAsync(el => el.UserEmail == userEmail);

			if (existedUser != null)
			{
				Console.WriteLine($"Пользователь найден.\n Id = {existedUser.Id}");
				return existedUser;
			}
			else
			{
				Console.WriteLine($"Пользователь не найден, возвращаем null");
				return null;
			}
		}

		public async Task<User?> GetUserById(int userId)
		{
			if (userId <= 0) return null;

			Console.WriteLine($"Поиск пользователя с id = {userId}");

			var existedUser = await _context.Users.FindAsync(userId);

			if (existedUser != null)
			{
				Console.WriteLine($"Пользователь найден.\n Id = {existedUser.Id}");
				return existedUser;
			}
			else
			{
				Console.WriteLine($"Пользователь не найден, возвращаем null");
				return null;
			}
		}

		public async Task<User?> GetUserByName(string userName)
		{
			if (string.IsNullOrEmpty(userName)) return null;

			Console.WriteLine($"Поиск пользователей с userName = {userName}");

			var existedUser = await _context.Users.FirstOrDefaultAsync(el => el.UserName == userName);

			if (existedUser != null)
			{
				Console.WriteLine($"Пользователь найден.\n Id = {existedUser.Id}");
				return existedUser;
			}
			else
			{
				Console.WriteLine($"Пользователь не найден, возвращаем null");
				return null;
			}
		}

		public async Task<User?> UpdateUser(int userId, string newUserName, string newUserEmail)
		{
			if(userId <= 0)
			{
				Console.WriteLine($"Передан некорректный идентификатор пользователя");
				return null;
			}

			if(string.IsNullOrEmpty(newUserName))
			{
				Console.WriteLine($"Передано пустое поле name, изменение невозможно");
				return await GetUserById(userId);
			}

			if (string.IsNullOrEmpty(newUserEmail))
			{
				Console.WriteLine($"Передано пустое поле email, изменение невозможно");
				return await GetUserById(userId);
			}

			var user = await GetUserById(userId);
			
			// Получаем совпадения по введенным данным
			var receiveUserByName = await GetUserByName(newUserName);
			var receiveUserByEmail = await GetUserByEmail(newUserEmail);

			// Учитываем, что могут быть введены те же данные, что были изначально (ничего не изменилось)
			if(user != null && (receiveUserByName == null || receiveUserByName.Id == userId) && (receiveUserByEmail == null || receiveUserByEmail.Id == userId))
			{
				Console.WriteLine($"Изменяем данные пользователя");

				user.UserName = newUserName;
				user.UserEmail = newUserEmail;
				user.IsApprove = false;

				await _context.SaveChangesAsync();
				Console.WriteLine($"Изменение выполнено успешно");
				return user;
			}

			Console.WriteLine($"Преданы некорректные данные, либо, указанные данные для изменения уже используются у других пользователей. \n userId = {userId}, найденный пользователь - {user?.Id}\n newUserName = {newUserName}, найденный пользователь - {receiveUserByName?.Id}\n newUserEmail = {newUserEmail}, найденный пользователь - {receiveUserByEmail?.Id}");

			return null;
		}

		public string? GeneratePassword(string innerString)
		{
			using var hmac = new System.Security.Cryptography.HMACSHA256(System.Text.Encoding.UTF8.GetBytes(_secretKeyToPwd));
			var hashBytes = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(innerString));
			return Convert.ToBase64String(hashBytes);
		}
	}
}
