using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Xefiros.Shared.Models;

namespace Xefiros.Server.Helpers
{
    public class IdentityProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, $"{user.Nombre} {user.Apellidos}"),
                new Claim(ClaimTypes.Email, user.UserName)
            };

            context.IssuedClaims.AddRange(claims);

            await Task.CompletedTask;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var usuarioId = context.Subject.GetSubjectId(); // Obtiene el Id del Usuario

            var usuario = await _userManager.FindByIdAsync(usuarioId);

            context.IsActive = usuario is not null;
        }
    }
}