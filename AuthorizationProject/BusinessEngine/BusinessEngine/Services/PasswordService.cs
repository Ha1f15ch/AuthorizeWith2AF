using System.Security.Cryptography;
using BusinessEngine.Services.Interfaces;

namespace BusinessEngine.Services;

public class PasswordService : IPasswordService
{
    private const int SaltSize = 16;
    private const int HashSize = 20;
    private const int HashIterations = 10000;
    
    public string? HashPassword(string? password)
    {
        try
        {
            using var algorithm = new Rfc2898DeriveBytes(
                password,
                SaltSize,
                HashIterations,
                HashAlgorithmName.SHA256);
        
            byte[] hash = algorithm.GetBytes(HashSize);
            byte[] salt = algorithm.Salt;

            byte[] hashWithSalt = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashWithSalt, 0, SaltSize);
            Array.Copy(hash, 0, hashWithSalt, SaltSize, HashSize);

            return Convert.ToBase64String(hashWithSalt);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Возникла ошибка при хешировании пароля пользователя, ошибка - {e.Message}");
            return null;
        }
    }

    public bool VerifyHashedPassword(string hashedPassword, string innerPassword)
    {
        try
        {
            byte[] hashWithSalt = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashWithSalt, 0, salt, 0, SaltSize);

            using var algorithm = new Rfc2898DeriveBytes(
                innerPassword,
                salt,
                HashIterations,
                HashAlgorithmName.SHA256);

            byte[] expectedHash = algorithm.GetBytes(HashSize);

            for (int i = 0; i < HashSize; i++)
            {
                if (hashWithSalt[i + SaltSize] != expectedHash[i])
                    return false;
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Возникла ошибка при проверке хешей паролей, ошибка - {e.Message}");
            return false;
        }
    }
}