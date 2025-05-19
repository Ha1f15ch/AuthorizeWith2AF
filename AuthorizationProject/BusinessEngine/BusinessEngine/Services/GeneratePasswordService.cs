using BusinessEngine.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace BusinessEngine.Services
{
	public class GeneratePasswordService : IGeneratePassword
	{
		private readonly string _secretKey;

		public GeneratePasswordService(IConfiguration configuration)
		{
			_secretKey = configuration["SecretKeyForUserPwd"];
		}

		public string GeneratePassword(string password)
		{
			var salt = Encoding.UTF8.GetBytes(_secretKey);
			var hash = Rfc2898DeriveBytes.Pbkdf2(
				Encoding.UTF8.GetBytes(password), 
				salt,
				iterations: 10000,
				HashAlgorithmName.SHA256,
				outputLength: 32);

			return Convert.ToBase64String(hash);
		}
	}
}
