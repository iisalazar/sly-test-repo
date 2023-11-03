using Entities.DTOs.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts;

public interface IAuthenticationService
{
  Task<IdentityResult> RegisterUser(RegisterUserDto userDto);
  Task<TokensForAuthenticationDto> LoginUser(LoginUserDto userDto);

  Task<UserDto> GetUserPublicInfo(string username);
}