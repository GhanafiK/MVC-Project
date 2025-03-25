using BusinessLogicLayer.DTOs;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Factories
{
    static class DepartmentFactory
    {
        public static DepartmentDTO ToDepartmentDTO(this Department department)
        {
            return new DepartmentDTO()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = DateOnly.FromDateTime(department.CreatedOn)
            };
        }
        public static DepartmentDetailsDTO ToDepartmentDetailsDTO(this Department department)
        {
            return new DepartmentDetailsDTO()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = DateOnly.FromDateTime(department.CreatedOn),
                CreatedBy = department.CreatedBy,
                LastModifiedBy = department.LastModifiedBy,
                DateOfModification = DateOnly.FromDateTime(department.LastModifiedOn),
                IsDeleted = department.IsDeleted,
            };
        }
        public static Department ToEntity(this CreatedDepartmentDTO departmentDTO)
        {
            return new Department()
            {
                Name = departmentDTO.Name,
                Code = departmentDTO.Code,
                Description = departmentDTO.Description,
                CreatedOn = departmentDTO.DateOfCreation.ToDateTime(new TimeOnly())
            };
        } 
        public static Department ToEntity(this UpdatedDepartmentDTO departmentDTO)
        {
            return new Department()
            {
                Id = departmentDTO.Id,
                Name = departmentDTO.Name,
                Code = departmentDTO.Code,
                Description = departmentDTO.Description,
                CreatedOn = departmentDTO.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }
    }
}
