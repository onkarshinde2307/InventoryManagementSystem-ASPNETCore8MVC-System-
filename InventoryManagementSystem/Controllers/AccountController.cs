using System.Security.Claims;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthDAL _auth;

        public AccountController(DbConnection db)
        {
            _auth = new AuthDAL(db);
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            // Validate input
            //REGISTER USER IF invalid, return to view with validation messages
            if (!ModelState.IsValid) return View(model);

            var ok = _auth.Register(model.Username.Trim(), model.Password, model.FullName, model.Email);
            if (!ok)
            {
                ModelState.AddModelError(string.Empty, "Username already taken.");
                return View(model);
            }

            // optional: auto-login after register
            // here we just redirect to login page and let user login manually
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            var vm = new LoginViewModel { ReturnUrl = returnUrl };
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // Validate input
            // IF invalid, return to view with validation messages AND ADDED error message
            if (!ModelState.IsValid) return View(model);

            var user = _auth.Validate(model.Username.Trim(), model.Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("UserID", user.UserID.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //hERE =>  Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
