using BusinessLogicLayer.DTOs.EmployeeDTOs;
using BusinessLogicLayer.Services.Classes;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create(CreateEmployeeDTO data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int Result = _employeeService.AddEmployee(data);
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
            return View(data);
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

        #region Edit Department
        public IActionResult Edit(int? id)
        {
            if(!id.HasValue) return BadRequest();
            var Employee=_employeeService.GetEmployeeById(id.Value);
            return Employee is null ? NotFound() : View(new UpdatedEmployeeDTO()
            {
                Id = Employee.Id,
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
        public IActionResult Edit( [FromRoute]int? id,UpdatedEmployeeDTO updatedEmployeeDTO)
        {
            if(!id.HasValue || id!=updatedEmployeeDTO.Id) return BadRequest();
            if(ModelState.IsValid)
            {
                try
                {
                    var Result=_employeeService.UpdateEmployee(updatedEmployeeDTO);
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
            return View(updatedEmployeeDTO);
        }
        #endregion
    }
}
