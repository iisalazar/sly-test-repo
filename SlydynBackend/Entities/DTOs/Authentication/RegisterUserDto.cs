using System.ComponentModel.DataAnnotations;
using Entities.CustomValidations;

namespace Entities.DTOs.Authentication;

public record RegisterUserDto
{
  [Required(ErrorMessage = "Email is required")]
  public string? Email { get; init; }
  [Required(ErrorMessage = "Username is required")]
  public string? Username { get; init; }
  [Required(ErrorMessage = "Password is required")]
  public string? Password { get; init; }
  [Required(ErrorMessage = "Role is required")]
  [AllowedStringValue(AllowedValues = new string[]{ "Consumer", "Dealer", "SuperAdmin"})]
  public string? Role { get; init; }
  
}