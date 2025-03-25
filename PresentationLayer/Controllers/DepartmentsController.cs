using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

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
    }

}
