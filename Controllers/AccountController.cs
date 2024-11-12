using CustomAuth.Entities;
using CustomAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace CustomAuth.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public IActionResult Index()
        {
            return View(_context.UserAccounts.ToList());
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the email or username already exists
                bool emailExists = _context.UserAccounts.Any(u => u.Email == model.Email);
                bool usernameExists = _context.UserAccounts.Any(u => u.UserName == model.UserName);

                if (emailExists)
                {
                    ModelState.AddModelError("Email", "This email is already taken.");
                }

                if (usernameExists)
                {
                    ModelState.AddModelError("UserName", "This username is already taken.");
                }

                // If either exists, return the view with the error messages
                if (emailExists || usernameExists)
                {
                    return View(model);
                }

                var account = new UserAccount
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName
                };

                // Hash the password before saving
                var passwordHasher = new PasswordHasher<UserAccount>();
                account.Password = passwordHasher.HashPassword(account, model.Password);

                _context.UserAccounts.Add(account);
                _context.SaveChanges();

                TempData["SuccessMessage"] = $"{account.FirstName} {account.LastName} registered successfully. Please Login.";
                return RedirectToAction("Login");
            }

            return View(model);
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the user by username or email
                var user = _context.UserAccounts
                    .FirstOrDefault(x => x.UserName == model.UserNameOrEmail || x.Email == model.UserNameOrEmail);

                if (user != null)
                {
                    // Create an instance of PasswordHasher to verify the password
                    var passwordHasher = new PasswordHasher<UserAccount>();
                    var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);

                    if (passwordVerificationResult == PasswordVerificationResult.Success)
                    {
                        // Handle login success (e.g., set a cookie or token)
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("Name", user.FirstName),
                    new Claim(ClaimTypes.Role, "User"),
                };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                        return RedirectToAction("SecurePage");
                    }
                }

                ModelState.AddModelError("", "Username/Email or Password is not correct.");
            }

            return View(model);
        }

        public IActionResult logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name=HttpContext.User.Identities.First().Name;
            return View();
        }
    }
}
