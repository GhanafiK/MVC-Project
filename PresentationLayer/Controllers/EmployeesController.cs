using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService) : Controller
    {
        public IActionResult Index()
        {
            var Employees = _employeeService.GetAllEmployees();
            return View(Employees);
        }
    }
}
