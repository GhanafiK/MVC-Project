using BusinessLogicLayer.DTOs.EmployeeDTOs;
using DataAccessLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.ViewModels.RoleViewModel;
using PresentationLayer.ViewModels.UserViewModel;

namespace PresentationLayer.Controllers
{
    [Authorize]
    public class RolesController(RoleManager<IdentityRole> _roleManager, ILogger<EmployeesController> _logger, IWebHostEnvironment _environment, UserManager<ApplicationUser> _userManager) : Controller
    {
        #region Get All Roles 
        public async Task<IActionResult> Index(string? RoleSearchName)
        {
            var Roles = _roleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(RoleSearchName))
            {
                Roles = Roles.Where(R => R.Name.ToLower().Contains(RoleSearchName.ToLower()));
            }
            var RolesList = await Roles.Select(R => new RoleViewModel()
            {
                Id = R.Id,
                Name=R.Name ?? " "
            }).ToListAsync();

            return View(RolesList);
        }
        #endregion

        #region Create New Role

        public async Task<IActionResult> Create(){
            var Users = await _userManager.Users.ToListAsync();
            return View(new RoleViewModel()
            {
                Users = Users.Select(U => new UserRoleViewModel()
                {
                    UserId = U.Id,
                    UserName = U.UserName,
                    IsSelected = false // Default to not selected for new roles
                }).ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Create the new role
                    var role = new IdentityRole()
                    {
                        Name = roleViewModel.Name
                    };

                    var result = await _roleManager.CreateAsync(role);

                    if (result.Succeeded)
                    {
                        // Assign users to the role if selected
                        foreach (var userRole in roleViewModel.Users)
                        {
                            if (userRole.IsSelected)
                            {
                                var user = await _userManager.FindByIdAsync(userRole.UserId);
                                if (user != null)
                                {
                                    await _userManager.AddToRoleAsync(user, role.Name);
                                }
                            }
                        }

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        foreach (var err in result.Errors)
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

            // If we got this far, something failed; redisplay form
            return View(roleViewModel);
        }
     

        #endregion

        #region Details of Role
        public async Task<IActionResult> Details(string? id)
        {
            if (id is null) return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            var Users = await _userManager.Users.ToListAsync();
            if (role is null)
                return NotFound();
            var roleViewModel = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name ?? " ",
                Users = Users.Select(U => new UserRoleViewModel()
                {
                    UserId = U.Id,
                    UserName = U.UserName,
                    IsSelected = _userManager.IsInRoleAsync(U, role.Name).Result
                }).ToList()
            };
            return View(roleViewModel);
        }
        #endregion

        #region Edit Role
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null) return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            var Users=await _userManager.Users.ToListAsync();
            return role is null ? NotFound() : View(new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name ?? " ",
                Users=Users.Select(U=>new UserRoleViewModel()
                {
                    UserId= U.Id,
                    UserName=U.UserName,
                    IsSelected=_userManager.IsInRoleAsync(U,role.Name).Result
                }).ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string? id, RoleViewModel roleViewModel)
        {
            if (id is null) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    if (role is null) return NotFound();
                    role.Name= roleViewModel.Name;
                    var Result = await _roleManager.UpdateAsync(role);
                    foreach(var userRole in roleViewModel.Users)
                    {
                        var user = await _userManager.FindByIdAsync(userRole.UserId);
                        if(user is not null)
                        {
                            if(userRole.IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                                await _userManager.AddToRoleAsync(user,role.Name);
                            else if(!userRole.IsSelected && (await _userManager.IsInRoleAsync(user, role.Name)))
                                await _userManager.RemoveFromRoleAsync(user,role.Name);
                        }
                    }
                    if (Result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        foreach (var err in Result.Errors)
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
            return View(roleViewModel);
        }
        #endregion

        #region Delete Role
        public async Task<ActionResult> Delete(string? id)
        {
            if (id is null) return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound();
            return View(new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name ?? " "
            });
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            if (id is null) return BadRequest();
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is not null)
                {
                    await _roleManager.DeleteAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                {
                    ModelState.AddModelError(string.Empty, "An Error Happend when deleting the role !!!");
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
