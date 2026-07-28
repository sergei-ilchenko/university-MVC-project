using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Soft.Controllers;

public class LoginController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult LoginWithGoogle(string returnUrl = "/")
    {
        var redirectUrl = Url.Action("GoogleResponse", "Login", new { returnUrl });
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(GoogleDefaults.AuthenticationScheme, redirectUrl);
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet]
    public async Task<IActionResult> GoogleResponse(string returnUrl = "/")
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            return RedirectToAction("Login", "Account");
        }

        // Retrieve the email from the external login info
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);

        // Attempt to sign in the user with the external login info
        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
        if (result.Succeeded)
        {
            // Add the email to claims if not already present
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user != null)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                if (!claims.Any(c => c.Type == ClaimTypes.Email))
                {
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, email));
                }
            }

            return LocalRedirect(returnUrl);
        }

        // If the user does not exist, create a new account
        var userEmail = email;
        var newUser = new IdentityUser { UserName = userEmail, Email = userEmail };

        var createResult = await _userManager.CreateAsync(newUser);
        if (createResult.Succeeded)
        {
            await _userManager.AddLoginAsync(newUser, info);
            await _signInManager.SignInAsync(newUser, isPersistent: false);

            // Add the email as a claim
            await _userManager.AddClaimAsync(newUser, new Claim(ClaimTypes.Email, userEmail));

            return LocalRedirect(returnUrl);
        }

        // Handle errors during account creation
        return RedirectToAction("Login", "Account");
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout(string returnUrl = "/")
    {
           
        await _signInManager.SignOutAsync();

            
        TempData.Clear();

            
        return LocalRedirect(returnUrl);
    }
}