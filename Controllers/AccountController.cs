using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using SO53654020.Infrastructure;

namespace SO53654020.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string returnUrl) =>
            new ChallengeResult(
                GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action(nameof(LoginCallback), new { returnUrl })
                });

        public async Task<IActionResult> LoginCallback(string returnUrl)
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(Constants.SignInScheme);

            if (!authenticateResult.Succeeded)
                return BadRequest();

            var claimsIdentity = new ClaimsIdentity(Constants.ApplicationScheme);

            claimsIdentity.AddClaim(authenticateResult.Principal.FindFirst(ClaimTypes.NameIdentifier));
            claimsIdentity.AddClaim(authenticateResult.Principal.FindFirst(ClaimTypes.Email));

            await HttpContext.SignInAsync(
                Constants.ApplicationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties { IsPersistent = true }); // IsPersistent will set a cookie that lasts for two weeks (by default).

            return LocalRedirect(returnUrl);
        }
    }
}
