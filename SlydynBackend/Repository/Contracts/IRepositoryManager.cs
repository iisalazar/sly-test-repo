namespace Repository.Contracts;

public interface IRepositoryManager
{
  IRefreshTokenRepository RefreshTokenRepository { get; }
  IUserSessionRepository UserSessionRepository { get; }
  void Save();
  Task SaveAsync();
}