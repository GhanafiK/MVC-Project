using DataAccessLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.ViewModels.AccountViewModel;

namespace PresentationLayer.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager,SignInManager<ApplicationUser> _signInManager) : Controller
    {
        #region Register
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel ViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(ViewModel);
            }

            var User = new ApplicationUser()
            {
                UserName = ViewModel.UserName,
                LastName = ViewModel.LastName,
                FirstName = ViewModel.FirstName,
                Email = ViewModel.Email,
            };

            var Result = _userManager.CreateAsync(User, ViewModel.Password).Result;
            if (Result.Succeeded)
                return RedirectToAction("Login");
            else
            {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(ViewModel);
            }

        }
        #endregion

        #region Login
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);
            var user = _userManager.FindByEmailAsync(loginViewModel.Email).Result;
            if (user is not null)
            {
                var flag = _userManager.CheckPasswordAsync(user, loginViewModel.Password).Result;
                if (flag)
                {
                    // PasswordSignInAsync it make the token so it need the (TokenProviders) service to make the token encrypted
                    var Result = _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, true).Result;
                    if (Result.Succeeded) return RedirectToAction("Index", "Home");
                    if (Result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your Account is Not Allowed");
                    if (Result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your Account iS Locked Out");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Login");
            return View(loginViewModel);
        }
            #endregion
    }
}
