using BusinessEngine.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace BusinessEngine.Services
{
	public class GeneratePasswordService : IGeneratePassword
	{
		private readonly string _secretKey;
		private readonly char[] _passwordEntity =
		[
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
			'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
			'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'!', '@', '#', '$', '%', '^', '&', '*', '-', '_', '+', '='
		];

		private readonly int _passwordLength = 12;

		public GeneratePasswordService(IConfiguration configuration)
		{
			_secretKey = configuration["SecretKeyForUserPwd"];
		}

		public string? GeneratePassword()
		{
			try
			{
				var resultPassword = new StringBuilder();
				using var rng = RandomNumberGenerator.Create();
				
				var countChars = _passwordEntity.Length;
				var byteArray = new byte[_passwordLength];

				for (var i = 0; i < _passwordLength; i++)
				{
					rng.GetBytes(byteArray, 0, 1);
					var index = byteArray[0] % countChars;
					resultPassword.Append(_passwordEntity[index]);
				}
				
				return resultPassword.ToString();
			}
			catch (Exception e)
			{
				Console.WriteLine($"При генерации пароля возникла ошибка, ошибка - {e.Message}");
				return null;
			}
		}
	}
}
