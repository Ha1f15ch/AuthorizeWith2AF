using DatabaseEngine.DbModels;

namespace DatabaseEngine.RepositoryInterfaces
{
	public interface IRefreshTokenRepository
	{
		public Task<RefreshToken?> GetrefreshTokenById(int id);
		public Task<RefreshToken?> GetrefreshTokenByUserId(int userId);
		public Task<RefreshToken?> GenerateRefreshToken(int userId);
		public Task<bool> RemoveRefreshTokenById(int id);
		public Task<bool> CheckValidationToken(int id);
	}
}
