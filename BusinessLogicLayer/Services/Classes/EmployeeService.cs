using AutoMapper;
using BusinessLogicLayer.DTOs.EmployeeDTOs;
using BusinessLogicLayer.Factories;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Repositories.Classes;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository,IMapper _mapper) : IEmployeeService
    {
        public IEnumerable<EmployeeDTO> GetAllEmployees(bool withTracking=false)
        {
            var Employees = _employeeRepository.GetAll(withTracking);
            var EmployeesDTO = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDTO>>(Employees);
            return EmployeesDTO;
        }

        public EmployeeDetailsDTO? GetEmployeeById(int id)
        {
            var Employee = _employeeRepository.GetDepartmentById(id);
            return Employee is null ? null : _mapper.Map<Employee, EmployeeDetailsDTO>(Employee);
        }
        public bool DeleteEmployee(int id)
        {
            var Employee = _employeeRepository.GetDepartmentById(id);
            if (Employee is null) return false;
            else
            {
                Employee.IsDeleted = true;
                return _employeeRepository.Update(Employee)>0?true:false;
            }

        }

        public int AddEmployee(CreateEmployeeDTO createEmployeeDTO)=> _employeeRepository.Add(_mapper.Map<CreateEmployeeDTO,Employee>(createEmployeeDTO));
        

        public int UpdateEmployee(UpdatedEmployeeDTO updatedEmployeeDTO)=> _employeeRepository.Update(_mapper.Map<UpdatedEmployeeDTO,Employee>(updatedEmployeeDTO));
        
    }
}
