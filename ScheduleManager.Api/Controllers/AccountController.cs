using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using I = Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Api.Models.Account;
using ScheduleManager.Authentication.Identity;

namespace ScheduleManager.Api.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return LocalRedirect(returnUrl ?? "/");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return LocalRedirect(returnUrl ?? "/");

            if (!ModelState.IsValid)
                return View(model);

            var user = GetTemplateUser();
            if (await _userManager.FindByEmailAsync(user.Email) == null)
                await _userManager.CreateAsync(user, "Qweqwe123");

            if (await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true) == I.SignInResult.Success)
                return LocalRedirect(returnUrl ?? "/");

            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect(returnUrl ?? "/");
        }

        private ApplicationUser GetTemplateUser()
        {
            return new ApplicationUser
            {
                Email = "unsinedz@gmail.com",
                EmailConfirmed = true,
                UserName = "unsinedz"
            };
        }
    }
}