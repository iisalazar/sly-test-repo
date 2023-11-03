using Repository.Contracts;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
  private readonly RepositoryContext _context;
  private readonly Lazy<IRefreshTokenRepository> _refreshTokenRepository;

  public RepositoryManager(RepositoryContext context)
  {
    _context = context;
    _refreshTokenRepository = new Lazy<IRefreshTokenRepository>(() => new RefreshTokenRepository(context));
  }

  public IRefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository.Value;

  public void Save()
  {
    _context.SaveChanges();
  }

  public Task SaveAsync()
  {
    return _context.SaveChangesAsync();
  }
}