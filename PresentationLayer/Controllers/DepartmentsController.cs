using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.ViewModels.DepartmentViewModels;

namespace PresentationLayer.Controllers
{
    public class DepartmentsController(IDepartmentService _departmentService,ILogger<DepartmentsController> _logger,IWebHostEnvironment _environment):Controller
    {
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }

        #region Create Department
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDTO data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int Result = _departmentService.AddDepartment(data);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                        ModelState.AddModelError(string.Empty, "Can't Add Department");
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                        ModelState.AddModelError(string.Empty, ex.Message);
                    else
                        _logger.LogError(ex.Message);
                }
            }
            return View(data);
        }
        #endregion

        #region Details Of Department

        public IActionResult Details(int? id)
        {
            if (id is null) return BadRequest();
            var department=_departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            return View(department);
        }

        #endregion

        #region Edit Department
        public IActionResult Edit(int? id)
        {
            if(id is null) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            return View(new DepartmentEditViewModel()
            {
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.DateOfCreation,
            });
        }

        [HttpPost]
        public IActionResult Edit(DepartmentEditViewModel model, [FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var UpdatedDepartment = new UpdatedDepartmentDTO()
                    {
                        Id = id,
                        Name = model.Name,
                        Code = model.Code,
                        Description = model.Description,
                        DateOfCreation = model.DateOfCreation,
                    };
                    int Result = _departmentService.UpdateDepartment(UpdatedDepartment);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Can't Update The Department");
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
            return View(model);
        }

        #endregion

        #region Delete Department
        //public ActionResult Delete(int? id)
        //{
        //    if (id is null) return BadRequest();
        //    var department = _departmentService.GetDepartmentById(id.Value);
        //    if (department is null) return NotFound();
        //    return View(department);
        //}

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool deleted= _departmentService.DeleteDepartment(id);
                if (deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department is not deleted");
                    return RedirectToAction(nameof(Delete), new { id = id });
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
