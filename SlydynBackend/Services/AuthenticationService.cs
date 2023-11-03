using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Entities.CustomClaimTypes;
using Entities.DTOs.Authentication;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using Repository.Contracts;

namespace Services;

public class AuthenticationService: IAuthenticationService
{
  private readonly UserManager<User> _userManager;
  private readonly IConfiguration _configuration;
  private readonly IRepositoryManager _repository;
  private readonly IMapper _mapper;

  public AuthenticationService(
    IMapper mapper,
    IRepositoryManager repository, UserManager<User> userManager,
    IConfiguration configuration)
  {
    _mapper = mapper;
    _repository = repository;
    _userManager = userManager;
    _configuration = configuration;
  }

  public async Task<UserDto> GetUserPublicInfo(string username)
  {
    var user = await GetUser(username);
    if (user == null)
    {
      var message = $"{nameof(GetUserPublicInfo)}: User with username {username} not found.";
      throw new Exception(message);
    }
    
    // add comment

    var userDto = _mapper.Map<UserDto>(user);
    return userDto;
  }

  private async Task<User?> GetUser(string username)
  {
    var user = await _userManager.FindByNameAsync(username);
    return user;
  } 
  public async Task<IdentityResult> RegisterUser(RegisterUserDto userDto)
  {
    var user = _mapper.Map<User>(userDto);
    var result = await _userManager.CreateAsync(user, userDto.Password!);

    if (userDto.Role != null) await _userManager.AddToRoleAsync(user, userDto.Role);

    return result;
  }

  public async Task<TokensForAuthenticationDto> LoginUser(LoginUserDto userDto)
  {
    var user = await GetUser(userDto.UserName!);
    if (user == null)
    {
      var message = $"{nameof(LoginUser)}: Authentication failed. Wrong username or password.";
      // TODO: Add logger
      throw new Exception("Invalid username / password"); // TODO replace with custom exception
    }

    var passwordMatches = await _userManager.CheckPasswordAsync(user, userDto.Password!);

    if (!passwordMatches)
    {
      var message = $"{nameof(LoginUser)}: Authentication failed. Wrong username or password.";
      // TODO: Add logger
      throw new Exception("Invalid username / password"); // TODO replace with custom exception
    }

    return await IssueNewTokens(user);
  }

  private async Task<TokensForAuthenticationDto> IssueNewTokens(User user)
  {
    var claims = await GetClaims(user);
    var accessToken = CreateAccessToken(claims);
    string refreshToken = await CreateRefreshToken(user);
    return new TokensForAuthenticationDto
    {
      AccessToken = accessToken,
      RefreshToken = refreshToken
    };
  }

  private async Task<string> CreateRefreshToken(User user)
  {
    string refreshToken = Guid.NewGuid().ToString();
    var newRefreshToken = new UserRefreshToken
    {
      UserOwner = user,
      Blacklisted = false,
      TokenString = refreshToken,
      ExpiresAt = DateTime.Now.AddDays(5)
    };
    
    _repository.RefreshTokenRepository.CreateToken(newRefreshToken);
    await _repository.SaveAsync();
    return refreshToken;
  }

  private string CreateAccessToken(List<Claim> claims)
  {
    var signingCredentials = GetSigningCredentials();

    var tokenOptions = GenerateTokenOptions(
      signingCredentials,
      claims);

    return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
  }

  private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
  {
      
    var jwtSettings = _configuration.GetSection("JwtSettings");
    var tokenOptions = new JwtSecurityToken(

      issuer: jwtSettings["ValidIssuer"],
      audience: jwtSettings["ValidAudience"],
      claims: claims,
      expires: DateTime.Now.AddHours(Convert.ToDouble(jwtSettings["Expires"])),
      signingCredentials: signingCredentials
    );
    return tokenOptions;
  }
  private SigningCredentials GetSigningCredentials()
  {
    var jwtSettings = _configuration.GetSection("JwtSettings");
    var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);
    var secret = new SymmetricSecurityKey(key);
    return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
  }


  private async Task<List<Claim>> GetClaims(User user)
  {
    var claims = new List<Claim>
    {
      new Claim(ClaimTypes.Name, user.UserName),
      new Claim(CustomClaimTypes.UserId, user.Id)
    };
    var roles = await _userManager.GetRolesAsync(user);

    foreach (var role in roles)
    {
      claims.Add(new Claim(ClaimTypes.Role, role));
    }

    return claims;
  }
}