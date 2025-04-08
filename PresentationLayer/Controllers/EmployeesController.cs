using BusinessLogicLayer.DTOs.EmployeeDTOs;
using BusinessLogicLayer.Services.Classes;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.ViewModels.EmployeeViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PresentationLayer.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService, ILogger<EmployeesController> _logger, IWebHostEnvironment _environment) : Controller
    {
        public IActionResult Index()
        {
            var Employees = _employeeService.GetAllEmployees();
            return View(Employees);
        }

        #region Create Employee
        public IActionResult Create() => View();

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var CreatedEmployee=new CreateEmployeeDTO()
                    {
                        Name = model.Name,
                        Address = model.Address,
                        Age = model.Age,
                        Email = model.Email,
                        EmployeeType = model.EmployeeType,
                        Gender = model.Gender,
                        HiringDate = model.HiringDate,
                        IsActive = model.IsActive,
                        PhoneNumber = model.PhoneNumber,
                        Salary = model.Salary   
                        
                    };

                    int Result = _employeeService.AddEmployee(CreatedEmployee);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                        ModelState.AddModelError(string.Empty, "Can't Add Employee");
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

        #region Details of Employee
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var Employee= _employeeService.GetEmployeeById(id.Value);
            return Employee is null ? NotFound() : View(Employee);
        }
        #endregion

        #region Edit Employee
        public IActionResult Edit(int? id)
        {
            if(!id.HasValue) return BadRequest();
            var Employee=_employeeService.GetEmployeeById(id.Value);
            return Employee is null ? NotFound() : View(new EmployeeViewModel()
            {
                Address = Employee.Address,
                Age = Employee.Age,
                Email = Employee.Email,
                HiringDate= Employee.HiringDate,
                IsActive= Employee.IsActive,
                Name= Employee.Name,
                PhoneNumber= Employee.PhoneNumber,
                Salary= Employee.Salary,
                EmployeeType=Enum.Parse<EmployeeType>(Employee.EmployeeType),
                Gender=Enum.Parse<Gender>(Employee.Gender)
            });
        }

        [HttpPost]
        public IActionResult Edit( [FromRoute]int? id, EmployeeViewModel model)
        {
            if(!id.HasValue) return BadRequest();
            if(ModelState.IsValid)
            {
                try
                {
                    var UpdatedEmployee = new UpdatedEmployeeDTO()
                    {
                        Salary = model.Salary,
                        PhoneNumber = model.PhoneNumber,
                        IsActive = model.IsActive,
                        Name = model.Name,
                        HiringDate = model.HiringDate,
                        Gender = model.Gender,
                        EmployeeType = model.EmployeeType,
                        Address = model.Address,
                        Age = model.Age,
                        Email = model.Email,
                        Id =id.Value,
                    };
                    var Result=_employeeService.UpdateEmployee(UpdatedEmployee);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                        ModelState.AddModelError(string.Empty, "Can't Update Employee");   
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

        #region Delete Employee
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if(id==0) return BadRequest();
            try
            {
                bool deleted = _employeeService.DeleteEmployee(id);
                if (deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee is not deleted");
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
