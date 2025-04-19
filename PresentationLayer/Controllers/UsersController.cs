using BusinessLogicLayer.DTOs.EmployeeDTOs;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Models.IdentityModels;
using DataAccessLayer.Models.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PresentationLayer.ViewModels.EmployeeViewModels;
using PresentationLayer.ViewModels.UserViewModel;

namespace PresentationLayer.Controllers
{
    [Authorize]
    public class UsersController(UserManager<ApplicationUser> _userManager, ILogger<EmployeesController> _logger, IWebHostEnvironment _environment) : Controller
    {
        #region Get All Users 
        public async Task<IActionResult> Index(string? UserSearchName)
        {
            var Users = _userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(UserSearchName))
            {
                Users = Users.Where(x => x.UserName.ToLower().Contains(UserSearchName.ToLower()));
            }
            var UsersList = await Users.Select(U => new UserViewModel()
            {
                ID = U.Id,
                FirstName = U.FirstName,
                LastName = U.LastName ?? string.Empty,
                Email = U.Email ?? string.Empty,
                PhoneNumber = U.PhoneNumber ?? string.Empty,
            }).ToListAsync();

            foreach (var user in UsersList)
                user.Roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.ID));
            return View(UsersList);
        }
        #endregion

        #region Details of User
        public async Task<IActionResult> Details(string? id)
        {
            if (id is null) return BadRequest();
            var user =await _userManager.FindByIdAsync(id);
            if(user is null)
                return NotFound();
            var userViewModel = new UserViewModel()
            {
                ID = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName?? string.Empty,
                Email = user.Email ?? string.Empty,
                PhoneNumber=user.PhoneNumber ?? string.Empty,
                Roles=  _userManager.GetRolesAsync(user).Result
            };
            return View(userViewModel);
        }
        #endregion

        #region Edit User
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            return user is null ? NotFound() : View(new UserDetailsViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string? id, UserDetailsViewModel userDetailsViewModel)
        {
            if (id is null) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if(user is null) return NotFound();
                    user.FirstName=userDetailsViewModel.FirstName;
                    user.LastName=userDetailsViewModel.LastName;
                    user.PhoneNumber=userDetailsViewModel.PhoneNumber;
                    var Result = await _userManager.UpdateAsync(user);
                    if (Result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        foreach(var err in Result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, err.Description);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                        ModelState.AddModelError(string.Empty, ex.Message);
                    else
                        _logger.LogError(ex.Message);
                }

            }
            return View(userDetailsViewModel);
        }
        #endregion

        #region Delete User
        public async Task<ActionResult> Delete(string? id)
        {
            if (id is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound();
            return View(new UserViewModel()
            {
                ID= id,
                FirstName = user.FirstName,
                LastName = user.LastName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
            });
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            if (id is null) return BadRequest();
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if(user is not null)
                {
                    await _userManager.DeleteAsync(user);
                    return RedirectToAction(nameof(Index));
                }
                {
                    ModelState.AddModelError(string.Empty, "An Error Happend when deleting the user !!!");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }
        #endregion
    }
}
