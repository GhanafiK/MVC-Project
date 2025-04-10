using DataAccessLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.ViewModels.AccountViewModel;

namespace PresentationLayer.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager) : Controller
    {
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel ViewModel)
        {
            if(!ModelState.IsValid)
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

            var Result=_userManager.CreateAsync(User,ViewModel.Password).Result;
            if (Result.Succeeded)
                return RedirectToAction("Login");
            else
            {
                foreach(var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(ViewModel);  
            }

        }
    }
}
