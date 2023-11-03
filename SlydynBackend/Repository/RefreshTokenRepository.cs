using Entities.Models;
using Repository.Contracts;

namespace Repository;

public class RefreshTokenRepository: RepositoryBase<UserRefreshToken>, IRefreshTokenRepository
{
  
  public RefreshTokenRepository(RepositoryContext context) : base(context)
  {
  }
  public void CreateToken(UserRefreshToken token)
  {
    Create(token);   
  }
}