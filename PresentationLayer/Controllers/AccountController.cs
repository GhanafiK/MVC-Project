using DataAccessLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Utilities;
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

            var ExistUser=_userManager.FindByEmailAsync(User.Email).Result;

            if(ExistUser == null)
            {
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
            ModelState.AddModelError(string.Empty, "This Email is Registered Before");
            return View(ViewModel);
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
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(loginViewModel);
        }
        #endregion

        #region SignOut 

        public new IActionResult SignOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        #region Reset Password

        public IActionResult ForgetPassword() => View();

        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(viewModel.Email).Result;
                if (user != null)
                {
                    var Token=_userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { email = viewModel.Email, Token }, Request.Scheme)??" ";
                    var email = new Email()
                    {
                        TO = viewModel.Email,
                        Subject = "Reset Passord",
                        Body = ResetPasswordLink
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword),viewModel);
        }


        public IActionResult CheckYourInbox() => View();

        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            string email=TempData["email"] as string ?? string.Empty;
            var token=TempData["token"] as string ?? string.Empty;

            var user=_userManager.FindByEmailAsync(email).Result;
            if (user is not null)
            {
                var Result = _userManager.ResetPasswordAsync(user, token, viewModel.Password).Result;
                if (Result.Succeeded)
                    return RedirectToAction(nameof(Login));
                else
                    foreach(var err in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err.Description);
                    }
            }
            return View(viewModel);
        }
        #endregion
    }
}
