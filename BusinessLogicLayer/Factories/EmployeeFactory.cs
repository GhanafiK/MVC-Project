using BusinessLogicLayer.DTOs.EmployeeDTOs;
using DataAccessLayer.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Factories
{
    public static class EmployeeFactory
    {
        public static EmployeeDTO ToEmployeeDTO(this Employee employee)
        {
            return new EmployeeDTO()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
                IsActive = employee.IsActive,
                Email = employee.Email,
            };
        }
        public static EmployeeDetailsDTO ToEmployeeDetailsDTO(this Employee employee)
        {
            return new EmployeeDetailsDTO()
            {
                Id = employee.Id,
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                Salary = employee.Salary,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
                IsActive = employee.IsActive,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
            };
        }

        public static Employee ToEntity(this CreateEmployeeDTO createEmployeeDTO)
        {
            return new Employee()
            {
                Name = createEmployeeDTO.Name,
                Age = createEmployeeDTO.Age,
                Address = createEmployeeDTO.Address,
                IsActive = createEmployeeDTO.IsActive,
                Salary = createEmployeeDTO.Salary,
                Email = createEmployeeDTO.Email,
                PhoneNumber = createEmployeeDTO.PhoneNumber,
                HiringDate = createEmployeeDTO.HiringDate,
                Gender = createEmployeeDTO.Gender,
                EmployeeType = createEmployeeDTO.EmployeeType,
                CreatedBy = createEmployeeDTO.CreatedBy,
                LastModifiedBy = createEmployeeDTO.LastModifiedBy,
            };
        }

        public static Employee ToEntity(this UpdatedEmployeeDTO updatedEmployeeDTO)
        {
            return new Employee()
            {
                Id= updatedEmployeeDTO.Id,
                Name = updatedEmployeeDTO.Name,
                Age = updatedEmployeeDTO.Age,
                Address = updatedEmployeeDTO.Address,
                IsActive = updatedEmployeeDTO.IsActive,
                Salary = updatedEmployeeDTO.Salary,
                Email = updatedEmployeeDTO.Email,
                PhoneNumber = updatedEmployeeDTO.PhoneNumber,
                HiringDate = updatedEmployeeDTO.HiringDate,
                Gender = updatedEmployeeDTO.Gender,
                EmployeeType = updatedEmployeeDTO.EmployeeType,
                CreatedBy = updatedEmployeeDTO.CreatedBy,
                LastModifiedBy = updatedEmployeeDTO.LastModifiedBy,
            };
        }

    }
}
