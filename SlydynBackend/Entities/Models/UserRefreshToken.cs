using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class UserRefreshToken
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set;  }
  
  public User? UserOwner { get; set; }
  
  [Required]
  public String? TokenString { get; set; }

  [Required] public DateTime ExpiresAt { get; set; }

  [Required] public bool Blacklisted { get; set; } = false;
}