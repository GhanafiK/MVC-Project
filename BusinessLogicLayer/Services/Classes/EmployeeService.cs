using BusinessLogicLayer.DTOs.EmployeeDTOs;
using BusinessLogicLayer.Factories;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Repositories.Classes;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository) : IEmployeeService
    {
        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            var Employees = _employeeRepository.GetAll();
            return Employees.Select(E => E.ToEmployeeDTO());
        }

        public EmployeeDetailsDTO? GetEmployeeById(int id)
        {
            var Employee = _employeeRepository.GetDepartmentById(id);
            return Employee.ToEmployeeDetailsDTO();
        }

        public int AddEmployee(CreateEmployeeDTO createEmployeeDTO) => _employeeRepository.Add(createEmployeeDTO.ToEntity());

        public int UpdateEmployee(UpdatedEmployeeDTO updatedEmployeeDTO) => _employeeRepository.Update(updatedEmployeeDTO.ToEntity());

        public bool DeleteEmployee(int id)
        {
            var Employee = _employeeRepository.GetDepartmentById(id);
            if (Employee is null) return false;
            else
            {
                int Result = _employeeRepository.Remove(Employee);
                return Result > 0 ? true : false;
            }

        }
    }
}
