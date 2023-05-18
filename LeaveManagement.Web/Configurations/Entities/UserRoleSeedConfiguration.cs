using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Web.Configurations.Entities
{
    internal class UserRoleSeedConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "6ead16eb-a6a4-4e3d-a17f-09f839084ea3",
                    UserId = "f603bf5c-2604-4914-bc01-1988994a1dff",
                },
                new IdentityUserRole<string>
                {
                    RoleId = "1aed16eb-c7b4-4e3a-a17f-09f839084ad7",
                    UserId = "e303bf5c-2604-4914-bc01-1988991bc4e3",
                }
                );
        }
    }
}