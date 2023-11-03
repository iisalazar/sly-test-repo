namespace Entities.DTOs.Authentication;

public record TokensForAuthenticationDto
{
  public string? AccessToken { get; init; }
  public string? RefreshToken { get; init; }
  
  public int ExpiresIn { get; init; }
}