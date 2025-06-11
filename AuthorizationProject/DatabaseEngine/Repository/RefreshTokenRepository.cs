using System.Security.Cryptography;
using DatabaseEngine.DbModels;
using DatabaseEngine.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DatabaseEngine.Repository;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _context;

    public RefreshTokenRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<RefreshToken?> GetrefreshTokenById(int id)
    {
        return await _context.RefreshTokens.FindAsync(id);
    }

    public async Task<RefreshToken?> GetrefreshTokenByUserId(int userId) // нужно использовать кастомный вариант вывода данных, чтобы на другой стороне было понятно, какой конкретно результат был получен во-время поиска
    {
        var tokenValue = await _context.RefreshTokens.CountAsync(el => el.UserId == userId);
        if (tokenValue > 1)
        {
            Console.WriteLine($"В БД хранится некорректное значение токенов для авторизации пользователя");
            return null;
        }

        if (tokenValue == 0)
        {
            Console.WriteLine($"Нет авторизационных токенов у пользователя, необходимо выполнить повторный вход");
            return null;
        }
        
        return await _context.RefreshTokens.SingleOrDefaultAsync(el => el.UserId == userId);
    }

    public async Task<RefreshToken?> GenerateRefreshToken(int userId)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
        {
            Console.WriteLine($"Пользователя с переданным id пользователя не найдено");
            return null;
        }
        
        var existedToken = await GetrefreshTokenByUserId(userId);

        if (existedToken != null)
        {
            Console.WriteLine($"токен для данного пользователя уже формировался");
            var isValid = await CheckValidationToken(existedToken.Id);

            if (isValid)
            {
                return existedToken;
            }
            
            await RemoveRefreshTokenById(existedToken.Id); 
            return null;   
        }
        
        // Создаем модель для записи в БД 
        var newToken = new RefreshToken
        {
            UserId = userId,
            UniqueRefreshToken = GenerateRefreshToken(),
            DateCreated = DateTime.Now,
            DateExpired = DateTime.Now.AddDays(14)
        };
        
        await _context.RefreshTokens.AddAsync(newToken);
        await _context.SaveChangesAsync();
        
        return newToken;
    }

    public async Task<bool> RemoveRefreshTokenById(int id)
    {
        var token = await _context.RefreshTokens.FindAsync(id);

        if (token is not null)
        {
            _context.RefreshTokens.Remove(token);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Удалено успешно");
            
            return true;
        }

        Console.WriteLine("Удалить не получилось");
        return false;
    }

    public async Task<bool> CheckValidationToken(int id)
    {
        var token = await _context.RefreshTokens.FindAsync(id);

        if (token is null)
        {
            Console.WriteLine($"Токен при проверке времени активности равен null");
            throw new NullReferenceException($"Передано null значение");
        }
        
        //Проверяем активность
        var isValid = token.DateExpired < DateTime.Now;
        
        return isValid;
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}