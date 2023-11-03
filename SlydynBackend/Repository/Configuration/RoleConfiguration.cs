using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class RoleConfiguration: IEntityTypeConfiguration<IdentityRole>
{
  public void Configure(EntityTypeBuilder<IdentityRole> builder)
  {
    builder.HasData(
      new IdentityRole
      {
        Name = "Consumer",
        NormalizedName = "CONSUMER"
      },
      new IdentityRole
      {
        Name = "Dealer",
        NormalizedName = "DEALER"
      },
      new IdentityRole
      {
        Name = "SuperAdmin",
        NormalizedName = "SUPERADMIN"
      }
    );
  }
}