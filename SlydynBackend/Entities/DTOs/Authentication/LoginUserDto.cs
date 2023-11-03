using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs.Authentication;

public record LoginUserDto
{
  [Required(ErrorMessage = "Username is required")]
  public string? UserName { get; init; }
  [Required(ErrorMessage = "Password is required")]
  public string? Password {get; init; }
}