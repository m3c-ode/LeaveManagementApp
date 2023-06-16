using LeaveManagement.Common.Constants;
using LeaveManagement.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace LeaveManagement.Web.Services.Identity
{
    public class CustomClaimService : UserClaimsPrincipalFactory<Employee>
    {

        public CustomClaimService(
            UserManager<Employee> userManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(Employee user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim(UserClaims.FullName, $"{user.FirstName} {user.LastName}"));
            identity.AddClaim(new Claim(UserClaims.Email, user.Email));
            foreach (var role in await UserManager.GetRolesAsync(user))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            return identity;
        }
    }

}
