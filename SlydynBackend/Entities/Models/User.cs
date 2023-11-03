using Microsoft.AspNetCore.Identity;

namespace Entities.Models;

public class User: IdentityUser
{
  public List<UserRefreshToken> RefreshTokens { get; set; }
}