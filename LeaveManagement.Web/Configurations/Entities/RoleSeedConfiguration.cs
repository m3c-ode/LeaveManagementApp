using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Web.Configurations.Entities
{
    internal class RoleSeedConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "6ead16eb-a6a4-4e3d-a17f-09f839084ea3",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                },
                new IdentityRole
                {
                    Id = "1aed16eb-c7b4-4e3a-a17f-09f839084ad7",
                    Name = "User",
                    NormalizedName = "USER",
                },
                new IdentityRole
                {
                    Id = "3cae19ac-c7b1-4e4c-a17f-09f839353abc",
                    Name = "Public",
                    NormalizedName = "PUBLIC",
                }

            );
        }
    }
}