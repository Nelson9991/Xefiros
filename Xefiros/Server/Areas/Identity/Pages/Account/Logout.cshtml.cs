using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Xefiros.Shared.Models;

namespace Xefiros.Server.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private LinkGenerator _linkGenerator;

        public string IndexUri { get; set; }

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger,
            LinkGenerator linkGenerator)
        {
            _signInManager = signInManager;
            _logger = logger;
            _linkGenerator = linkGenerator;
        }

        public void OnGet()
        {
            IndexUri = _linkGenerator.GetUriByPage(HttpContext, "/");
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
