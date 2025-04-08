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
        public IActionResult Create(EmployeeViewModel CreatedEmployeeDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    #region For partial View
                    var createdEmployee = new CreateEmployeeDTO()
                    {
                        Name = CreatedEmployeeDTO.Name,
                        Address = CreatedEmployeeDTO.Address,
                        Age = CreatedEmployeeDTO.Age,
                        Email = CreatedEmployeeDTO.Email,
                        EmployeeType = CreatedEmployeeDTO.EmployeeType,
                        Gender = CreatedEmployeeDTO.Gender,
                        HiringDate = CreatedEmployeeDTO.HiringDate,
                        IsActive = CreatedEmployeeDTO.IsActive,
                        PhoneNumber = CreatedEmployeeDTO.PhoneNumber,
                        Salary = CreatedEmployeeDTO.Salary,
                        DepartmentId = CreatedEmployeeDTO.DepartmentId,

                    };
                    int Result = _employeeService.AddEmployee(createdEmployee);
                    #endregion

                    //int Result = _employeeService.AddEmployee(CreatedEmployeeDTO);
                    string msg;
                    if (Result > 0)
                        msg = $"Employee {CreatedEmployeeDTO.Name} is Added Successfully ";
                    
                    else
                        msg = $"Employee is not Added ";
                    TempData["EmpMessage"] = msg;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                        ModelState.AddModelError(string.Empty, ex.Message);
                    else
                        _logger.LogError(ex.Message);
                }
            }
            return View(CreatedEmployeeDTO);
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
            #region For partial View
            return Employee is null ? NotFound() : View(new EmployeeViewModel()
            {
                Address = Employee.Address,
                Age = Employee.Age,
                Email = Employee.Email,
                HiringDate = Employee.HiringDate,
                IsActive = Employee.IsActive,
                Name = Employee.Name,
                PhoneNumber = Employee.PhoneNumber,
                Salary = Employee.Salary,
                EmployeeType = Enum.Parse<EmployeeType>(Employee.EmployeeType),
                DepartmentId = Employee.DepartmentId,
                Gender = Enum.Parse<Gender>(Employee.Gender),
            });
            #endregion

            //return Employee is null ? NotFound() : View(new UpdatedEmployeeDTO()
            //{
            //    Id = Employee.Id,
            //    Address = Employee.Address,
            //    Age = Employee.Age,
            //    Email = Employee.Email,
            //    HiringDate = Employee.HiringDate,
            //    IsActive = Employee.IsActive,
            //    Name = Employee.Name,
            //    PhoneNumber = Employee.PhoneNumber,
            //    Salary = Employee.Salary,
            //    EmployeeType = Enum.Parse<EmployeeType>(Employee.EmployeeType),
            //    Gender = Enum.Parse<Gender>(Employee.Gender)
            //});
        }

        [HttpPost]
        public IActionResult Edit( [FromRoute]int? id, EmployeeViewModel updatedEmployeeDTO)
        {

            #region For partial view
            if (!id.HasValue) return BadRequest();
            #endregion
            //if (!id.HasValue || id != updatedEmployeeDTO.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    #region For partial view
                    var UpdatedEmployee = new UpdatedEmployeeDTO()
                    {
                        Salary = updatedEmployeeDTO.Salary,
                        PhoneNumber = updatedEmployeeDTO.PhoneNumber,
                        IsActive = updatedEmployeeDTO.IsActive,
                        Name = updatedEmployeeDTO.Name,
                        HiringDate = updatedEmployeeDTO.HiringDate,
                        Gender = updatedEmployeeDTO.Gender,
                        EmployeeType = updatedEmployeeDTO.EmployeeType,
                        Address = updatedEmployeeDTO.Address,
                        Age = updatedEmployeeDTO.Age,
                        Email = updatedEmployeeDTO.Email,
                        Id = id.Value,
                        DepartmentId= updatedEmployeeDTO.DepartmentId,
                    };

                    var Result = _employeeService.UpdateEmployee(UpdatedEmployee);
                    #endregion
                    //var Result =_employeeService.UpdateEmployee(updatedEmployeeDTO);
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
