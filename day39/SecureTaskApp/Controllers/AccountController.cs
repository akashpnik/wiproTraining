using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecureTaskApp.Data;
using SecureTaskApp.Models;
using System.Security.Claims;

namespace SecureTaskApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly SignInManager<ApplicationUser> _signInMgr;
        private readonly RoleManager<IdentityRole> _roleMgr;

        public AccountController(
            UserManager<ApplicationUser> userMgr,
            SignInManager<ApplicationUser> signInMgr,
            RoleManager<IdentityRole> roleMgr)
        {
            _userMgr = userMgr;
            _signInMgr = signInMgr;
            _roleMgr = roleMgr;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = new ApplicationUser { UserName = vm.Email, Email = vm.Email, EmailConfirmed = true };
            var result = await _userMgr.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var e in result.Errors) ModelState.AddModelError("", e.Description);
                return View(vm);
            }

            // Role assignment (default to User; toggle only for demo)
            var role = vm.RegisterAsAdmin ? "Admin" : "User";
            if (!await _roleMgr.RoleExistsAsync(role))
                await _roleMgr.CreateAsync(new IdentityRole(role));

            await _userMgr.AddToRoleAsync(user, role);

            // Give standard users the CanEditTask claim (per user story)
            if (role == "User")
            {
                await _userMgr.AddClaimAsync(user, new Claim("permission", "CanEditTask"));
            }

            await _signInMgr.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Dashboard", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = await _userMgr.FindByEmailAsync(vm.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials.");
                return View(vm);
            }

            var result = await _signInMgr.PasswordSignInAsync(user, vm.Password, vm.RememberMe, lockoutOnFailure: true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid credentials.");
                return View(vm);
            }

            if (!string.IsNullOrWhiteSpace(vm.ReturnUrl) && Url.IsLocalUrl(vm.ReturnUrl))
                return Redirect(vm.ReturnUrl);

            return RedirectToAction("Dashboard", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInMgr.SignOutAsync(); // invalidates cookie
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
