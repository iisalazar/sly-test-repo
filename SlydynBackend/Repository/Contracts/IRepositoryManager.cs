namespace Repository.Contracts;

public interface IRepositoryManager
{
  IRefreshTokenRepository RefreshTokenRepository { get; }
  void Save();
  Task SaveAsync();
}