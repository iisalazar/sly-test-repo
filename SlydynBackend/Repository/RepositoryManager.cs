using Microsoft.Extensions.Caching.Distributed;
using Repository.Contracts;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
  private readonly RepositoryContext _context;
  private readonly Lazy<IRefreshTokenRepository> _refreshTokenRepository;
  private readonly Lazy<IUserSessionRepository> _userSessionRepository;

  public RepositoryManager(RepositoryContext context, IDistributedCache cache)
  {
    _context = context;
    _refreshTokenRepository = new Lazy<IRefreshTokenRepository>(() => new RefreshTokenRepository(context));
    _userSessionRepository = new Lazy<IUserSessionRepository>(() => new UserSessionRepository(cache));
  }

  public IRefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository.Value;
  public IUserSessionRepository UserSessionRepository => _userSessionRepository.Value;
  
  public void Save()
  {
    _context.SaveChanges();
  }

  public Task SaveAsync()
  {
    return _context.SaveChangesAsync();
  }
}