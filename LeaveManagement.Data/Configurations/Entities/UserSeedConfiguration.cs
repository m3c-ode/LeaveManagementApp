using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Data.Configurations.Entities
{
    public class UserSeedConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            var hasher = new PasswordHasher<Employee>();
            builder.HasData(
                        new Employee
                        {
                            Id = "f603bf5c-2604-4914-bc01-1988994a1dff",
                            Email = "admin@localhost.com",
                            NormalizedEmail = "ADMIN@LOCALHOST.COM",
                            UserName = "admin@localhost.com",
                            NormalizedUserName = "ADMIN@LOCALHOST.COM",
                            FirstName = "System",
                            LastName = "Admin",
                            PasswordHash = hasher.HashPassword(null, "aA!111"),
                            EmailConfirmed = true,

                        },
                        new Employee
                        {
                            Id = "e303bf5c-2604-4914-bc01-1988991bc4e3",
                            Email = "user@localhost.com",
                            NormalizedEmail = "USER@LOCALHOST.COM",
                            UserName = "user@localhost.com",
                            NormalizedUserName = "USER@LOCALHOST.COM",
                            FirstName = "System",
                            LastName = "User",
                            PasswordHash = hasher.HashPassword(null, "aA!111"),
                            EmailConfirmed = true,

                        }
                    );
        }
    }
}