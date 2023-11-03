using Entities.Models;

namespace Repository.Contracts;

public interface IRefreshTokenRepository
{
  void CreateToken(UserRefreshToken token);
}